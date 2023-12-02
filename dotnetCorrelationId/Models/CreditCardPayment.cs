namespace dotnetCorrelationId.Models
{
    public class CreditCardPayment : IPayment
    {
        public string  ProcessPayment()
        {
            return "Pagamento com cartão de crédito realizado com sucesso!";
        }
    }
}
