namespace ProjectBank.BusinessLogic.Security.CVV
{
    public interface ICVVGenerator
    {
        string GenerateCVV(string cardNumber, DateTime expirationDate);
    }
}