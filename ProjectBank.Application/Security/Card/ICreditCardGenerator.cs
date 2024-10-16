namespace ProjectBank.BusinessLogic.Security.Card
{
    public interface ICreditCardGenerator
    {
        string GenerateCardNumber(string prefix = "4411", int length = 16);
    }
}