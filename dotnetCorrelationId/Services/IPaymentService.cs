using dotnetCorrelationId.Models.Enums;

namespace dotnetCorrelationId.Services;

public interface IPaymentService
{
    string ProcessPayment(PaymentMethod paymentMethod);
}
