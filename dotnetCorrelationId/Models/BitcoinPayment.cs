namespace dotnetCorrelationId.Models
{
    public class BitcoinPayment : IPayment
    {
        public string  ProcessPayment()
        {
            return "Pagamento com Bitcoin realizado com sucesso!";
        }
    }
}
