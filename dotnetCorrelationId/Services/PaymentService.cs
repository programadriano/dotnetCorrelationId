using dotnetCorrelationId.Infra;
using dotnetCorrelationId.Models;
using dotnetCorrelationId.Models.Enums;

namespace dotnetCorrelationId.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BaseLogger<PaymentService> _logger;
        private readonly Dictionary<PaymentMethod, IPayment> _paymentProcessors;

        public PaymentService(
            BaseLogger<PaymentService> logger,
            CreditCardPayment creditCardProcessor,
            PayPalPayment payPalProcessor,
            BitcoinPayment bitcoinProcessor)
        {
            _logger = logger;

            _paymentProcessors = new Dictionary<PaymentMethod, IPayment>
            {
                { PaymentMethod.CartaoCredito, creditCardProcessor },
                { PaymentMethod.PayPal, payPalProcessor },
                { PaymentMethod.BTC, bitcoinProcessor }
            };
        }

        public string ProcessPayment(PaymentMethod paymentMethod)
        {
            if (_paymentProcessors.TryGetValue(paymentMethod, out var paymentProcessor))
            {
                var result = paymentProcessor.ProcessPayment();
                _logger.LogInformation(result);
                return result;
            }

            _logger.LogError($"Método de pagamento inválido");
            return "Método de pagamento inválido.";
        }


    }

}
