using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Lambdy.Performance.Benchmarks.EfCoreContext;

namespace Lambdy.Performance.Benchmarks.Benchmarks.IterativeVsRecursive
{
    [SimpleJob(RunStrategy.ColdStart)]
    public class IterativeVsRecursiveBenchmark
    {
        private readonly List<long> _ids;
        public IterativeVsRecursiveBenchmark()
        {
            _ids = new List<long>() {2, 3, 5};
        }

        [Benchmark]
        public string RecursiveStringBuilderQueryCompiler()
        {
            LambdyQuery.SwitchCompiler(QueryCompilerAlgorithm.Recursive);
            return RunLambdy();
        }
        
        [Benchmark]
        public string RecursiveInterpolationQueryCompiler()
        {
            LambdyQuery.SwitchCompiler(QueryCompilerAlgorithm.Iterative);
            return RunLambdy();
        }
        
        private string RunLambdy()
        {
            var builder = LambdyQuery.ByModel(new
            {
                OrderDetailAlias = (OrderDetail)null,
                OrderAlias = (Order)null,
                CustomerAlias = (Customer)null,
                EmployeeAlias = (Employee)null
            })
                .Where(x => x.OrderAlias.ShipName.Contains("HA"))
                .Where(x => x.OrderAlias.EmployeeId == 2)
                .Where(x => _ids.Contains(x.OrderAlias.Id) ||
                            x.CustomerAlias.City.Contains("245"))
                .Where(x => x.OrderDetailAlias.UnitPrice > 5 &&
                            x.OrderDetailAlias.UnitPrice <= 10)
                .Where(x => x.EmployeeAlias.Title == "Junior")
                .Where(x => x.EmployeeAlias.Country == "Some")
                .Where(x => x.EmployeeAlias.Region == "Some2")
                .Where(x => x.EmployeeAlias.FirstName == "Name1")
                .Where(x => x.EmployeeAlias.LastName == "Surname2")
                .Where(x => x.EmployeeAlias.PostalCode == "Code")
                .Where(x => x.EmployeeAlias.TitleOfCourtesy == "Courtesy")
                .Where(x => x.EmployeeAlias.Notes == "notes231")
                .Where(x => x.OrderDetailAlias.Quantity == 15)
                .Where(x => x.CustomerAlias.City == "City223")
                .Where(x => x.CustomerAlias.Phone == "SomePhone")
                .Where(x => x.CustomerAlias.CompanyName == "CompanyName")
                .Where(x => x.CustomerAlias.Country == "Country1")
                .Where(x => x.CustomerAlias.Address == "Address2")
                .Where(x => x.CustomerAlias.ContactName == "NameOfContact")
                .Where(x => x.CustomerAlias.ContactTitle == "Title1")
                .Where(x => x.CustomerAlias.PostalCode == "PS code")
                .Where(x => x.CustomerAlias.Fax == "Fax")
                .Where(x => x.OrderAlias.ShipCity == "ShipCity")
                .Where(x => x.OrderAlias.ShipRegion == "ShipRegion")
                .Where(x => x.OrderAlias.ShipPostalCode == "ShipPostalCode")
                .Where(x => x.OrderAlias.ShipVia == 2)
                .Where(x => x.OrderAlias.OrderDate == "22.22.2012")
                .Where(x => x.OrderAlias.RequiredDate == "24.22.2012")
                .Where(x => x.OrderAlias.ShippedDate == "21.22.2012")
                .Where(x => x.OrderAlias.Freight == 4)
                .Where(x => x.OrderAlias.ShipName == "ShipName");

            var compiled = builder.Compile().Sql;
            return compiled;
        }
    }
}