using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;
using MatchArena.Domain.Settings.Stripes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace MatchArena.Persistence.Implementations.Services
{
    internal class PaymentService : IPaymentService
    {
        private readonly StripeSetting _stripeSetting;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public PaymentService(
            IOptions<StripeSetting> stripeOptions,
            IPaymentRepository paymentRepository,
            IProductRepository productRepository,
            IFieldRepository fieldRepository,
            ITournamentRepository tournamentRepository)
        {
            _stripeSetting = stripeOptions.Value;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _fieldRepository = fieldRepository;
            _tournamentRepository = tournamentRepository;

            StripeConfiguration.ApiKey = _stripeSetting.SecretKey;
        }

        public async Task<(Payment payment, string sessionUrl)> InitiatePaymentAsync(string userId, PaymentType type, long sourceId)
        {
            decimal amount = await ResolveAmountAsync(type, sourceId);

            var existingPayment = await _paymentRepository.GetAll(
                func: p => p.UserId == userId &&
                           p.Type == type &&
                           p.RelatedEntityId == sourceId &&
                           p.Status == PaymentStatus.Pending
            ).FirstOrDefaultAsync();

            Payment payment;
            if (existingPayment != null)
            {
                payment = existingPayment;
            }
            else
            {
                payment = new Payment
                {
                    UserId = userId,
                    Amount = amount,
                    Currency = "azn",
                    Type = type,
                    RelatedEntityId = sourceId,
                    Status = PaymentStatus.Pending
                };
                _paymentRepository.Add(payment);
                await _paymentRepository.SaveChangesAsync();
            }

            var sessionOptions = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(payment.Amount * 100),
                            Currency = payment.Currency.ToLower(),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = ResolvePaymentLabel(type)
                            }
                        },
                        Quantity = 1,
                    }
                },
                SuccessUrl = $"{_stripeSetting.SuccessUrl}?paymentId={payment.Id}",
                CancelUrl = _stripeSetting.CancelUrl,
                Metadata = new Dictionary<string, string>
                {
                    { "paymentId", payment.Id.ToString() }
                }
            };

            var stripeSessionService = new SessionService();
            var stripeSession = stripeSessionService.Create(sessionOptions);

            payment.StripeSessionId = stripeSession.Id;
            _paymentRepository.Update(payment);
            await _paymentRepository.SaveChangesAsync();

            return (payment, stripeSession.Url);
        }

        public async Task<bool> ValidateAndApproveAsync(long paymentId)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            if (payment == null) return false;

            if (string.IsNullOrEmpty(payment.StripeSessionId)) return false;

            var stripeSessionService = new SessionService();
            var stripeSession = stripeSessionService.Get(payment.StripeSessionId);

            if (stripeSession.PaymentStatus != "paid") return false;

            payment.Status = PaymentStatus.Confirmed;
            _paymentRepository.Update(payment);
            await _paymentRepository.SaveChangesAsync();

            await GrantOwnershipAsync(payment);

            return true;
        }

        private async Task GrantOwnershipAsync(Payment payment)
        {
            switch (payment.Type)
            {
                case PaymentType.Product:
                    break;
                case PaymentType.Tournament:
                    break;
                case PaymentType.Field:
                    break;
            }
        }

        private async Task<decimal> ResolveAmountAsync(PaymentType type, long sourceId)
        {
            switch (type)
            {
                case PaymentType.Product:
                    var product = await _productRepository.GetByIdAsync(sourceId);
                    if (product == null) throw new Exception("Məhsul tapılmadı.");
                    return product.Price;

                case PaymentType.Field:
                    var field = await _fieldRepository.GetByIdAsync(sourceId);
                    if (field == null) throw new Exception("Meydança tapılmadı.");
                    return field.PricePerHour;

                case PaymentType.Tournament:
                    var tournament = await _tournamentRepository.GetByIdAsync(sourceId);
                    if (tournament == null) throw new Exception("Turnir tapılmadı.");
                    return tournament.EntryFee;

                default:
                    throw new Exception("Naməlum ödəniş növü.");
            }
        }

        public async Task<Payment?> GetPaymentAsync(long paymentId)
            => await _paymentRepository.GetByIdAsync(paymentId);

        public async Task<List<Payment>> GetAllPaymentsAsync()
            => await _paymentRepository.GetAll(sort: p => p.Id, isDesc: true).ToListAsync();

        public async Task<List<Payment>> GetUserPaymentsAsync(string userId)
            => await _paymentRepository.GetAll(
                func: p => p.UserId == userId,
                sort: p => p.Id,
                isDesc: true
            ).ToListAsync();

        private string ResolvePaymentLabel(PaymentType type) => type switch
        {
            PaymentType.Product => "Məhsul alışı",
            PaymentType.Tournament => "Turnir iştirak haqqı",
            PaymentType.Field => "Meydança rezervasiyası",
            _ => "Ödəniş"
        };
    }
}