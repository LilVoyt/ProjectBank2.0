using Newtonsoft.Json.Linq;

namespace ProjectBank.BusinessLogic.Features.Currency
{
    public interface ICurrencyHandler
    {
        JObject GetFromApi();
    }
}