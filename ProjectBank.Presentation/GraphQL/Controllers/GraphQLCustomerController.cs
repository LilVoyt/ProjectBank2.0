//using GraphQL.Types;
//using GraphQL;
//using Microsoft.AspNetCore.Mvc;

//namespace ProjectBank.Presentation.GraphQL.Controllers
//{
//    [Route("graphql")]
//    [ApiController]
//    public class GraphQLCustomerController : ControllerBase
//    {
//        private readonly ISchema _schema;
//        private readonly IDocumentExecuter _executer;
//        private readonly ILogger<GraphQLCustomerController> _logger;

//        public GraphQLCustomerController(ISchema schema, IDocumentExecuter executer, ILogger<GraphQLCustomerController> logger)
//        {
//            _schema = schema;
//            _executer = executer;
//            _logger = logger;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
//        {
//            if (query == null) { return BadRequest("Query cannot be null."); }

//            var inputs = query.Variables.ToInputs();
//            var executionOptions = new ExecutionOptions
//            {
//                Schema = _schema,
//                Query = query.Query,
//                Variables = inputs
//            };

//            var result = await _executer.ExecuteAsync(executionOptions).ConfigureAwait(false);

//            if (result.Errors?.Count > 0)
//            {
//                _logger.LogError("GraphQL execution errors: {Errors}", result.Errors);
//                return BadRequest(result);
//            }

//            return Ok(result);
//        }

//    }
//}
