using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.Features.Currency
{
    public class CurrencyHandler : ICurrencyHandler
    {
        public JObject GetFromApi()
        {
            var client = new RestClient("https://api.currencyapi.com/v3/latest");

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "cur_live_fC3YUWtrfS6J60HWoPMxOfliB5RFfglFc0gxEznJ");
            IRestResponse response = client.Execute(request);
            var jsonResponse = JObject.Parse(response.Content);
            Console.WriteLine(response.Content);
            return jsonResponse;
        }

        public decimal getCurrecnyByCode(string Code)
        {
            var client = new RestClient("https://api.currencyapi.com/v3/latest");

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "cur_live_fC3YUWtrfS6J60HWoPMxOfliB5RFfglFc0gxEznJ");
            IRestResponse response = client.Execute(request);
            JObject jsonResponse = JObject.Parse(response.Content);
            
            decimal newCode = jsonResponse["data"][Code]["value"].ToObject<decimal>();
            return newCode;   
        }
    }
}
