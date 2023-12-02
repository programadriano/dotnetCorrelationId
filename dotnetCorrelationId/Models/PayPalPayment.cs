namespace dotnetCorrelationId.Models
{
    public class PayPalPayment : IPayment
    {
        public string  ProcessPayment()
        {
            return "Pagamento com PayPal realizado com sucesso!";
        }
    }
}
