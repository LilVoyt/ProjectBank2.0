//using GraphQL;
//using Newtonsoft.Json.Linq;

//namespace ProjectBank.Presentation.GraphQL.Controllers
//{
//    public class GraphQLQuery
//    {
//        public string OperationName { get; set; }
//        public string Query { get; set; }
//        public JObject Variables { get; set; }
//    }
//    public static class JObjectExtensions
//    {
//        public static Inputs ToInputs(this JObject variables)
//        {
//            var inputs = new Inputs(new Dictionary<string, object?>());
//            if (variables != null)
//            {
//                foreach (var property in variables.Properties())
//                {
//                    inputs.TryAdd(property.Name, property.Value.ToObject<object>());
//                }
//            }
//            return inputs;
//        }
//    }
//}
