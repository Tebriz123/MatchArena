using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchArena.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("field/{reservationId}")]
        public async Task<IActionResult> PayForField(Guid reservationId, [FromQuery] decimal fieldPrice)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payment = await _paymentService.InitiateFieldReservationPaymentAsync(userId, reservationId, fieldPrice);
            var session = await _paymentService.CreateCheckoutSessionAsync(
                payment.Id,
                $"{Request.Scheme}://{Request.Host}/api/payment/success?paymentId={payment.Id}",
                $"{Request.Scheme}://{Request.Host}/api/payment/cancel"
            );
            return Ok(new { sessionUrl = session.Url });
        }

        [HttpPost("tournament/{tournamentId}")]
        public async Task<IActionResult> PayForTournament(Guid tournamentId, [FromQuery] decimal entryFee, [FromQuery] bool isTeamCaptain)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payment = await _paymentService.InitiateTournamentPaymentAsync(userId, tournamentId, entryFee, isTeamCaptain);
            var session = await _paymentService.CreateCheckoutSessionAsync(
                payment.Id,
                $"{Request.Scheme}://{Request.Host}/api/payment/success?paymentId={payment.Id}",
                $"{Request.Scheme}://{Request.Host}/api/payment/cancel"
            );
            return Ok(new { sessionUrl = session.Url });
        }

        [HttpPost("product/{productId}")]
        public async Task<IActionResult> PayForProduct(Guid productId, [FromQuery] decimal productPrice)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var payment = await _paymentService.InitiateProductPaymentAsync(userId, productId, productPrice);
            var session = await _paymentService.CreateCheckoutSessionAsync(
                payment.Id,
                $"{Request.Scheme}://{Request.Host}/api/payment/success?paymentId={payment.Id}",
                $"{Request.Scheme}://{Request.Host}/api/payment/cancel"
            );
            return Ok(new { sessionUrl = session.Url });
        }

        [HttpGet("success")]
        public async Task<IActionResult> PaymentSuccess([FromQuery] long paymentId)
        {
            var result = await _paymentService.ValidateAndApproveAsync(paymentId);
            if (!result)
                return BadRequest("Ödəniş təsdiqlənmədi.");

            return Ok("Ödəniş uğurla tamamlandı.");
        }

        [HttpGet("cancel")]
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
