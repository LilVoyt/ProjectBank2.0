namespace ProjectBank.BusinessLogic.Models
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string NumberCard { get; set; }
        public string CardName { get; set; }
        public string Pincode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; }
        public decimal Balance { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
