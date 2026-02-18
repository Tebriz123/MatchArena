using MatchArena.Domain.Entities;
using MatchArena.Domain.Entities.Enums;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(string userId, PaymentType type, Guid entityId, decimal amount, string currency = "azn");
        Task<Payment> InitiateFieldReservationPaymentAsync(string userId, Guid reservationId, decimal fieldPrice);
        Task<Payment> InitiateTournamentPaymentAsync(string userId, Guid tournamentId, decimal entryFee, bool isTeamCaptain);
        Task<Payment> InitiateProductPaymentAsync(string userId, Guid productId, decimal productPrice);
        Task<Session> CreateCheckoutSessionAsync(long paymentId, string successUrl, string cancelUrl);
        Task<Payment?> GetPaymentAsync(long paymentId);
        bool CheckStripeSessionPaid(string sessionId);
        Task ConfirmPaymentAsync(Payment targetPayment);
        Task<bool> ValidateAndApproveAsync(long paymentId);
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<List<Payment>> GetUserPaymentsAsync(string userId);



    }
}
