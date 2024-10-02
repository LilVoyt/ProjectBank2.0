namespace ProjectBank.BusinessLogic.Models
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<CardDto> Cards { get; set; }
    }
}
