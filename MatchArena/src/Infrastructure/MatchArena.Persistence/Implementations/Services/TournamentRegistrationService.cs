using AutoMapper;
using MatchArena.Application.DTOs.TournamentRegistrations;
using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

internal class TournamentRegistrationService : ITournamentRegistrationService
{
    private readonly ITournamentRegistrationRepository _registrationRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public TournamentRegistrationService(
        ITournamentRegistrationRepository registrationRepository,
        ITournamentRepository tournamentRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
        IPaymentService paymentService,
        IMapper mapper)
    {
        _registrationRepository = registrationRepository;
        _tournamentRepository = tournamentRepository;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _paymentService = paymentService;
        _mapper = mapper;
    }

    public async Task<(long registrationId, string sessionUrl)> RegisterAsync(
        PostTournamentRegistrationDto dto, string userId)
    {
        Tournament tournament = await _tournamentRepository.GetByIdAsync(dto.TournamentId);
        if (tournament is null)
            throw new Exception("Turnir tapılmadı");

        if (tournament.Status != TournamentStatus.RegistrationOpen)
            throw new Exception("Turnirin qeydiyyatı açıq deyil");

        if (DateTime.UtcNow > tournament.RegistrationDeadline)
            throw new Exception("Qeydiyyat müddəti keçib");

        if (tournament.CurrentTeams >= tournament.MaxTeams)
            throw new Exception("Turnir doludur");

        Team team = await _teamRepository.GetByIdAsync(dto.TeamId, "TeamPlayers");
        if (team is null)
            throw new Exception("Komanda tapılmadı");

        Player player = _playerRepository.GetAll(p => p.UserId == userId).FirstOrDefault()
            ?? throw new Exception("Siz player deyilsiniz");

        bool isCaptain = team.TeamPlayers.Any(tp => tp.PlayerId == player.Id && tp.IsCaptain);
        if (!isCaptain)
            throw new Exception("Yalnız komanda kapitanı turnirə qeydiyyatdan keçə bilər");

        bool alreadyRegistered = _registrationRepository.GetAll(
            r => r.TournamentId == dto.TournamentId &&
                 r.TeamId == dto.TeamId &&
                 r.Status != RegistrationStatus.Cancelled
        ).Any();

        if (alreadyRegistered)
            throw new Exception("Bu komanda artıq turnirə qeydiyyatdadır");

        var registration = new TournamentRegistration
        {
            TournamentId = dto.TournamentId,
            TeamId = dto.TeamId,
            CaptainUserId = userId,
            Status = RegistrationStatus.Pending
        };

        _registrationRepository.Add(registration);
        await _registrationRepository.SaveChangesAsync();

        var (payment, sessionUrl) = await _paymentService.InitiatePaymentAsync(
            userId, PaymentType.Tournament, dto.TournamentId);

        registration.PaymentId = payment.Id;
        _registrationRepository.Update(registration);
        await _registrationRepository.SaveChangesAsync();

        return (registration.Id, sessionUrl);
    }

    public async Task ConfirmRegistrationAsync(long paymentId)
    {
        var registration = _registrationRepository.GetAll(
            r => r.PaymentId == paymentId
        ).FirstOrDefault();

        if (registration is null) return;

        Tournament tournament = await _tournamentRepository.GetByIdAsync(registration.TournamentId);
        if (tournament is null) return;

        tournament.CurrentTeams++;
        registration.Status = RegistrationStatus.Confirmed;

        if (tournament.CurrentTeams >= tournament.MaxTeams)
            tournament.Status = TournamentStatus.SlotsFull;

        if (DateTime.UtcNow > tournament.RegistrationDeadline)
            tournament.Status = TournamentStatus.RegistrationClosed;

        _registrationRepository.Update(registration);
        _tournamentRepository.Update(tournament);
        await _registrationRepository.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<GetTournamentRegistrationDto>> GetTournamentTeamsAsync(long tournamentId)
    {
        var registrations = await _registrationRepository.GetAll(
            func: r => r.TournamentId == tournamentId && r.Status == RegistrationStatus.Confirmed,
            includes: "Team,Tournament"
        ).ToListAsync();

        return _mapper.Map<IReadOnlyList<GetTournamentRegistrationDto>>(registrations);
    }

    public async Task CancelRegistrationAsync(long id, string userId)
    {
        var registration = await _registrationRepository.GetByIdAsync(id, "Tournament");
        if (registration is null)
            throw new Exception("Qeydiyyat tapılmadı");

        if (registration.CaptainUserId != userId)
            throw new Exception("Yalnız kapitan qeydiyyatı ləğv edə bilər");

        if (registration.Status == RegistrationStatus.Confirmed)
        {
            registration.Tournament.CurrentTeams--;

            if (registration.Tournament.Status == TournamentStatus.SlotsFull)
                registration.Tournament.Status = TournamentStatus.RegistrationOpen;

            _tournamentRepository.Update(registration.Tournament);
        }

        registration.Status = RegistrationStatus.Cancelled;
        _registrationRepository.Update(registration);
        await _registrationRepository.SaveChangesAsync();
    }
}