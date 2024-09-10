//using GraphQL;
//using GraphQL.Types;
//using Microsoft.Extensions.DependencyInjection;
//using ProjectBank.Presentation.GraphQL.Queries;
//using ProjectBank.Presentation.GraphQL.Mutations;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using SchemaType = GraphQL.Types.Schema;

//namespace ProjectBank.Presentation.GraphQL.Schema
//{
//    public class ProjectBankSchema : SchemaType
//    {
//        public ProjectBankSchema(IServiceProvider serviceProvider) : base(serviceProvider)
//        {
//            Query = serviceProvider.GetRequiredService<CustomerQuery>();
//            Mutation = serviceProvider.GetRequiredService<CustomerMutation>();
//        }
//    }
//}
