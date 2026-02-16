using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Settings.Stripes;
using MatchArena.Persistence.Contexts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Services
{
    //internal class StripeService: IStripeService
    //{
    //    private readonly StripeSetting _stripeSettings;
    //    private readonly AppDbContext _dbContext;

    //    public StripeService(IOptions<StripeSetting> stripeOptions, AppDbContext dbContext)
    //    {
    //        _stripeSettings = stripeOptions.Value;
    //        _dbContext = dbContext;


    //        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    //    }

    //    public Payment CreatePayment(string userId, PaymentType type, decimal amount, string currency = "usd")
    //    {
    //        amount = type switch
    //        {
    //            PaymentType.Product => 20m,
    //            PaymentType.Tarot => 5m,
    //            PaymentType.Coffee => 5m,
    //            PaymentType.Horoscope => 5m,
    //            PaymentType.Palm => 5m,
    //            _ => 0m
    //        };
    //        var payment = new Payment
    //        {
    //            UserId = userId,
    //            Amount = amount,
    //            Currency = currency,
    //            Type = type,
    //            Status = PaymentStatus.Pending
    //        };

    //        _dbContext.Payments.Add(payment);
    //        _dbContext.SaveChanges();
    //        return payment;
    //    }


    //    public Session CreateCheckoutSession(long paymentId, string successUrl, string cancelUrl)
    //    {
    //        var payment = _dbContext.Payments.FirstOrDefault(p => p.Id == paymentId);
    //        if (payment == null)
    //            throw new Exception("Payment tapılmadı");

    //        var options = new SessionCreateOptions
    //        {
    //            PaymentMethodTypes = new List<string> { "card" },
    //            Mode = "payment",
    //            LineItems = new List<SessionLineItemOptions>
    //         {
    //             new SessionLineItemOptions
    //             {
    //                 PriceData = new SessionLineItemPriceDataOptions
    //                 {
    //                     UnitAmount = (long)(payment.Amount * 100),
    //                     Currency = payment.Currency.ToLower(),
    //                     ProductData = new SessionLineItemPriceDataProductDataOptions
    //                     {
    //                         Name = $"Ödəniş - {payment.Type}"
    //                     }
    //                 },
    //                 Quantity = 1,
    //             }
    //         },
    //            SuccessUrl = successUrl,
    //            CancelUrl = cancelUrl,
    //            Metadata = new Dictionary<string, string>
    //         {
    //             { "paymentId", paymentId.ToString() }
    //         }
    //        };

    //        var service = new SessionService();
    //        var session = service.Create(options);

    //        payment.StripeSessionId = session.Id;
    //        _dbContext.SaveChanges();

    //        return session;
    //    }

    //    public Payment GetPayment(long paymentId)
    //    {
    //        return _dbContext.Payments.FirstOrDefault(p => p.Id == paymentId);
    //    }

    //    public bool IsPaymentPaid(string stripeSessionId)
    //    {
    //        if (string.IsNullOrEmpty(stripeSessionId))
    //            return false;

    //        var service = new SessionService();
    //        var session = service.Get(stripeSessionId);

    //        return session.PaymentStatus == "paid";
    //    }

    //    public void ConfirmPayment(Payment payment)
    //    {
    //        if (payment == null)
    //            return;

    //        if (payment.Status == PaymentStatus.Confirmed)
    //            return;

    //        payment.Status = PaymentStatus.Confirmed;


    //        _dbContext.SaveChanges();
    //    }

    //    public bool VerifyAndConfirm(long paymentId)
    //    {
    //        var payment = GetPayment(paymentId);
    //        if (payment == null)
    //            return false;

    //        bool paid = IsPaymentPaid(payment.StripeSessionId);

    //        if (paid)
    //        {
    //            ConfirmPayment(payment);
    //            return true;
    //        }

    //        return false;
    //    }

    //    public decimal GetPriceForPaymentType(PaymentType type)
    //    {
    //        var price = _dbContext.PaymentTypePrices.FirstOrDefault(p => p.Type == type);
    //        if (price == null) throw new Exception($"Qiymət tapılmadı: {type}");
    //        return price.Price;
    //    }


    //    public List<Payment> GetAllPayments()
    //    {
    //        return _dbContext.Payments
    //            .OrderByDescending(p => p.Id)
    //            .ToList();
    //    }

    //    public List<Payment> GetUserPayments(string userId)
    //    {
    //        return _dbContext.Payments
    //            .Where(p => p.UserId == userId)
    //            .OrderByDescending(p => p.Id)
    //            .ToList();
    //    }

    //    public void CancelPayment(long paymentId)
    //    {
    //        var payment = _dbContext.Payments.FirstOrDefault(p => p.Id == paymentId);
    //        if (payment == null)
    //            throw new Exception("Payment not found");

    //        if (payment.Status == PaymentStatus.Paid)
    //            throw new Exception("Already paid, cannot cancel");

    //        payment.Status = PaymentStatus.Cancelled;
    //        _dbContext.Payments.Update(payment);
    //        _dbContext.SaveChanges();
    //    }
    //}
}
