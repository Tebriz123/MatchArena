using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<(Payment payment, string sessionUrl)> InitiatePaymentAsync(string userId, PaymentType type, long sourceId);
        Task<bool> ValidateAndApproveAsync(long paymentId);
        Task<Payment?> GetPaymentAsync(long paymentId);
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<List<Payment>> GetUserPaymentsAsync(string userId);
    }
}
//Task<Payment> CreatePaymentAsync(string userId, PaymentType type, decimal amount, long? entityId = null, long? productId = null, string currency = "azn");
//Task<Payment> InitiateFieldReservationPaymentAsync(string userId, long reservationId, decimal fieldPrice);
//Task<Payment> InitiateTournamentPaymentAsync(string userId, long tournamentId, decimal entryFee, bool isTeamCaptain);
//Task<Payment> InitiateProductPaymentAsync(string userId, long productId, decimal productPrice);
//Task<Session> CreateCheckoutSessionAsync(long paymentId, string successUrl, string cancelUrl);
//Task<Payment?> GetPaymentAsync(long paymentId);
//bool CheckStripeSessionPaid(string sessionId);
//Task ConfirmPaymentAsync(Payment targetPayment);
//Task<bool> ValidateAndApproveAsync(long paymentId);
//Task<List<Payment>> GetAllPaymentsAsync();
//Task<List<Payment>> GetUserPaymentsAsync(string userId);