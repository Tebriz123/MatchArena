using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{type}/{sourceId}")]
        public async Task<IActionResult> InitiatePayment(PaymentType type, long sourceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (payment, sessionUrl) = await _paymentService.InitiatePaymentAsync(userId, type, sourceId);
            return Ok(new { paymentId = payment.Id, sessionUrl });
        }

        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymentSuccess([FromQuery] long paymentId)
        {
            var result = await _paymentService.ValidateAndApproveAsync(paymentId);
            if (!result)
                return BadRequest("Ödəniş təsdiqlənmədi.");

            return Ok("Ödəniş uğurla tamamlandı.");
        }

        [HttpGet("cancel")]
        [AllowAnonymous]
        public IActionResult PaymentCancel()
        {
            return Ok("Ödəniş ləğv edildi.");
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPayment(long paymentId)
        {
            var payment = await _paymentService.GetPaymentAsync(paymentId);
            if (payment == null)
                return NotFound("Ödəniş tapılmadı.");

            return Ok(payment);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyPayments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payments = await _paymentService.GetUserPaymentsAsync(userId);
            return Ok(payments);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
    }
}