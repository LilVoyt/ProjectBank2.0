namespace ProjectBank.BusinessLogic.Models
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string NumberCard { get; set; }
        public string CardName { get; set; }
        public int Pincode { get; set; }
        public DateTime Data { get; set; }
        public int CVV { get; set; }
        public double Balance { get; set; }
    }
}
