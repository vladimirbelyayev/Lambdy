using System.Linq;
using BenchmarkDotNet.Attributes;
using Dapper;
using Lambdy.Constants;
using Lambdy.Performance.Benchmarks.EfCoreContext;
using Lambdy.Performance.Benchmarks.SqlLite;
using Microsoft.EntityFrameworkCore;

namespace Lambdy.Performance.Benchmarks.Benchmarks.SimpleAliasQuery
{
    //[SimpleJob(launchCount: 1, warmupCount: 3, targetCount: 5, invocationCount:100, id: "QuickJob")]
    public class SimpleAliasQueryBenchmark
    {
        private readonly long[] _orderIds = new long[]
        {
            10248,
            10249,
            10250,
            10251,
            10254,
            10267,
            10285,
            10318,
            10336,
            10349,
            10365,
            10373,
            10377,
            10398,
            10418,
            10432,
            10454,
            10476,
            10487,
            10505
        };
        
        [Benchmark(Baseline = true)]
        public int DapperAliasedOrderDetailQuery()
        {
            var counter = 0;
            foreach (var seedValue in _orderIds)
            {
                using var dbConnection = SqlLiteConnectionFactory.CreateConnection();
                dbConnection.Open();
                
                var dynParams = new DynamicParameters();
                dynParams.Add("@OrderId", seedValue);
        
                var res = dbConnection
                    .Query<SimpleAliasQueryResult>(
                        "SELECT Alias.Id AS NewId FROM [OrderDetail] AS Alias WHERE Alias.OrderId = @OrderId ",
                        dynParams);
                
                var list = res.ToList();
                counter += list.Count;
            }
        
            return counter;
        }
        
        [Benchmark]
        public int LambdyAliasedOrderDetailQuery()
        {
            var counter = 0;
            foreach (var seedValue in _orderIds)
            {
                using var dbConnection = SqlLiteConnectionFactory.CreateConnection();
                dbConnection.Open();

                var query = LambdyQuery
                    .ByModel(new { Alias = (OrderDetail) null })
                    .WithTemplate($"{LambdyTemplateTokens.Select} FROM [OrderDetail] AS Alias {LambdyTemplateTokens.Where}")
                    .Where(x => x.Alias.OrderId == seedValue)
                    .Select(x => new
                    {
                        NewId = x.Alias.Id
                    })
                    .Compile();
                
                var dynParams = new DynamicParameters(query.Parameters);

                var res = dbConnection
                    .Query<SimpleAliasQueryResult>(query.Sql, dynParams);
                
                var list = res.ToList();
                counter += list.Count;
            }

            return counter;
        }
        
        
        [Benchmark]
        public int EfCoreAliasedOrderDetailQuery()
        {
            var counter = 0;
            foreach (var seedValue in _orderIds)
            {
                using var ef = new NorthwindContext();
            
                var res = ef.OrderDetails
                    .Select(x => new { Alias = x })
                    .Where(x => x.Alias.OrderId == seedValue)
                    .Select(x => new SimpleAliasQueryResult
                    {
                        NewId = x.Alias.Id
                    })
                    .AsNoTracking();
                
                var list = res.ToList();
                counter += list.Count;
            }
        
            return counter;
        }
    }
}