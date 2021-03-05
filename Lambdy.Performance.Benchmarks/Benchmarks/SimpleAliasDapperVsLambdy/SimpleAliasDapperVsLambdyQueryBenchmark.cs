using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Dapper;
using Lambdy.Constants;
using Lambdy.Performance.Benchmarks.EfCoreContext;
using Lambdy.Performance.Benchmarks.SqlLite;

namespace Lambdy.Performance.Benchmarks.Benchmarks.SimpleAliasDapperVsLambdy
{
    public class SimpleAliasDapperVsLambdyQueryBenchmark
    {
        [Params(10250, 10476)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public long OrderId { get; set; }
        
        [Benchmark(Baseline = true)]
        public List<SimpleAliasDapperVsLambdyQueryResult> DapperAliasedOrderDetailQuery()
        {
            using var dbConnection = SqlLiteConnectionFactory.CreateConnection();
            dbConnection.Open();
                
            var dynParams = new DynamicParameters();
            dynParams.Add("@OrderId", OrderId);
        
            var res = dbConnection
                .Query<SimpleAliasDapperVsLambdyQueryResult>(
                    "SELECT Alias.Id AS NewId FROM [OrderDetail] AS Alias WHERE Alias.OrderId = @OrderId ",
                    dynParams);
                
            return res.ToList();
        }
        
        [Benchmark]
        public List<SimpleAliasDapperVsLambdyQueryResult> LambdyAliasedOrderDetailQuery()
        {
            using var dbConnection = SqlLiteConnectionFactory.CreateConnection();
            dbConnection.Open();

            var query = LambdyQuery
                .ByModel(new { Alias = (OrderDetail) null })
                .WithTemplate($"{LambdyTemplateTokens.Select} FROM [OrderDetail] AS Alias {LambdyTemplateTokens.Where}")
                .Where(x => x.Alias.OrderId == OrderId)
                .Select(x => new
                {
                    NewId = x.Alias.Id
                })
                .Compile();
                
            var dynParams = new DynamicParameters(query.Parameters);

            var res = dbConnection
                .Query<SimpleAliasDapperVsLambdyQueryResult>(query.Sql, dynParams);
                
            return res.ToList();
        }
    }
}