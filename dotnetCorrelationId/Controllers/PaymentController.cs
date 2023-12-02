using dotnetCorrelationId.Infra;
using dotnetCorrelationId.Request;
using dotnetCorrelationId.Services;
using Microsoft.AspNetCore.Mvc;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController(
        BaseLogger<PaymentController> logger,
        IPaymentService paymentProcessorService
    ) : ControllerBase
    {
        private readonly BaseLogger<PaymentController> _logger = logger;
        private readonly IPaymentService _paymentProcessorService = paymentProcessorService;


        [HttpPost]
        public IActionResult ProcessPayment([FromBody] PaymentRequest payment)
        {
            _logger.LogInformation($"Iniciando processo de pagamento");

            var result = _paymentProcessorService.ProcessPayment(payment.Method);

            _logger.LogInformation(result);

            return Ok(new { msg = result });
        }
    }
}
