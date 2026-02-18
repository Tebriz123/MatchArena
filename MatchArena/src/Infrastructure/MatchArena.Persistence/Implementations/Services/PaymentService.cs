using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Repositories.Generic;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;
using MatchArena.Domain.Settings.Stripes;
using MatchArena.Persistence.Contexts;
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

        public PaymentService(
            IOptions<StripeSetting> stripeOptions,
            IPaymentRepository paymentRepository)
        {
            _stripeSetting = stripeOptions.Value;
            _paymentRepository = paymentRepository;

            StripeConfiguration.ApiKey = _stripeSetting.SecretKey;
        }

        public async Task<Payment> CreatePaymentAsync(string userId, PaymentType type, Guid entityId, decimal amount, string currency = "azn")
        {
            var newPayment = new Payment
            {
                UserId = userId,
                Amount = amount,
                Currency = currency,
                Type = type,
                RelatedEntityId = entityId,
                Status = PaymentStatus.Pending
            };

            _paymentRepository.Add(newPayment);
            await _paymentRepository.SaveChangesAsync();
            return newPayment;
        }

        public async Task<Payment> InitiateFieldReservationPaymentAsync(string userId, Guid reservationId, decimal fieldPrice)
        {
            return await CreatePaymentAsync(userId, PaymentType.Field, reservationId, fieldPrice);
        }

        public async Task<Payment> InitiateTournamentPaymentAsync(string userId, Guid tournamentId, decimal entryFee, bool isTeamCaptain)
        {
            if (!isTeamCaptain)
                throw new UnauthorizedAccessException("Turnir ödənişini yalnız komanda kapitanı edə bilər.");

            var existingPayment = await _paymentRepository.GetAll(
                func: p => p.UserId == userId &&
                           p.Type == PaymentType.Tournament &&
                           p.RelatedEntityId == tournamentId &&
                           p.Status != PaymentStatus.Failed
            ).FirstOrDefaultAsync();

            if (existingPayment != null)
                return existingPayment;

            return await CreatePaymentAsync(userId, PaymentType.Tournament, tournamentId, entryFee);
        }

        public async Task<Payment> InitiateProductPaymentAsync(string userId, Guid productId, decimal productPrice)
        {
            return await CreatePaymentAsync(userId, PaymentType.Product, productId, productPrice);
        }

        public async Task<Session> CreateCheckoutSessionAsync(long paymentId, string successUrl, string cancelUrl)
        {
            var targetPayment = await _paymentRepository.GetByIdAsync(paymentId);
            if (targetPayment == null)
                throw new Exception("Ödəniş tapılmadı.");

            if (targetPayment.Status == PaymentStatus.Confirmed)
                throw new Exception("Bu ödəniş artıq tamamlanıb.");

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
                        UnitAmount = (long)(targetPayment.Amount * 100),
                        Currency = targetPayment.Currency.ToLower(),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = ResolvePaymentLabel(targetPayment.Type)
                        }
                    },
                    Quantity = 1,
                }
            },
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Metadata = new Dictionary<string, string>
            {
                { "paymentId", paymentId.ToString() }
            }
            };

            var stripeSessionService = new SessionService();
            var stripeSession = stripeSessionService.Create(sessionOptions);

            targetPayment.StripeSessionId = stripeSession.Id;
            _paymentRepository.Update(targetPayment);
            await _paymentRepository.SaveChangesAsync();

            return stripeSession;
        }

        public async Task<Payment?> GetPaymentAsync(long paymentId)
        {
            return await _paymentRepository.GetByIdAsync(paymentId);
        }

        public bool CheckStripeSessionPaid(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return false;

            var stripeSessionService = new SessionService();
            var stripeSession = stripeSessionService.Get(sessionId);

            return stripeSession.PaymentStatus == "paid";
        }

        public async Task ConfirmPaymentAsync(Payment targetPayment)
        {
            if (targetPayment == null) return;
            if (targetPayment.Status == PaymentStatus.Confirmed) return;

            targetPayment.Status = PaymentStatus.Confirmed;
            _paymentRepository.Update(targetPayment);
            await _paymentRepository.SaveChangesAsync();
        }

        public async Task<bool> ValidateAndApproveAsync(long paymentId)
        {
            var targetPayment = await GetPaymentAsync(paymentId);
            if (targetPayment == null) return false;

            bool isPaid = CheckStripeSessionPaid(targetPayment.StripeSessionId);
            if (isPaid)
            {
                await ConfirmPaymentAsync(targetPayment);
                return true;
            }

            return false;
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAll(
                sort: p => p.Id,
                isDesc: true
            ).ToListAsync();
        }

        public async Task<List<Payment>> GetUserPaymentsAsync(string userId)
        {
            return await _paymentRepository.GetAll(
                func: p => p.UserId == userId,
                sort: p => p.Id,
                isDesc: true
            ).ToListAsync();
        }

        private string ResolvePaymentLabel(PaymentType type) => type switch
        {
            PaymentType.Product => "Məhsul alışı",
            PaymentType.Tournament => "Turnir iştirak haqqı",
            PaymentType.Field => "Meydança rezervasiyası",
            _ => "Ödəniş"
        };
    }
}
