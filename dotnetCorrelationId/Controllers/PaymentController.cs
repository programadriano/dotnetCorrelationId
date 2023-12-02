using dotnetCorrelationId.Infra;
using dotnetCorrelationId.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController(
        ICorrelationIdGenerator correlationIdGenerator,
        ILogger<PaymentController> logger
    ) : ControllerBase
    {
        private readonly ICorrelationIdGenerator _correlationIdGenerator = correlationIdGenerator;
        private readonly ILogger<PaymentController> _logger = logger;

        [HttpPost]
        public IActionResult ProcessPayment([FromBody] Payment payment)
        {
            var correlationId = _correlationIdGenerator.Get();
            _logger.LogInformation($"Iniciando processo de pagamento {correlationId}");

            var result = payment.Method switch
            {
                "CartaoCredito"
                    => (
                        "Método de pagamento selecionado: Cartão de crédito",
                        "Pagamento com cartão de crédito realizado com sucesso!"
                    ),
                "PayPal"
                    => (
                        "Método de pagamento selecionado: PayPal",
                        "Pagamento com PayPal realizado com sucesso!"
                    ),
                "Bitcoin"
                    => (
                        "Método de pagamento selecionado: Bitcoin",
                        "Pagamento com Bitcoin realizado com sucesso!"
                    ),
                _ => ("Método de pagamento inválido.", "Método de pagamento inválido.")
            };

            _logger.LogInformation(result.Item1 + " - " + correlationId);
            return Ok(new { msg = result.Item2 });
        }
    }
}
