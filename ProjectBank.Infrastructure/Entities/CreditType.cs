namespace ProjectBank.DataAcces.Entities
{
    public class CreditType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal InterestRateMultiplier { get; set; }
        public string Description { get; set; }
        public decimal MaxCreditLimit { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
