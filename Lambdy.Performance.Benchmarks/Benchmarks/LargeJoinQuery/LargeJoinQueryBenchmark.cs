using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Dapper;
using Lambdy.Performance.Benchmarks.EfCoreContext;
using Lambdy.Performance.Benchmarks.SqlLite;
using Microsoft.EntityFrameworkCore;

namespace Lambdy.Performance.Benchmarks.Benchmarks.LargeJoinQuery
{
    public class LargeJoinQueryBenchmark
    {
        private const int PassIterations = 1;

        private const string DapperSql = @"SELECT 
                                    Product.Id AS ProductId, 
                                    OrderDetail.Id AS OrderDetailId, 
                                    OrderTable.Id AS OrderId, 
                                    Supplier.Id AS SupplierId, 
                                    Category.Id AS CategoryId, 
                                    Employee.Id AS EmployeeId,
                                    EmployeeTerritory.Id AS EmployeeTerritoryId,
                                    Territory.Id AS TerritoryId,
                                    Customer.Id AS CustomerId
                                    FROM Product Product
                                    INNER JOIN OrderDetail OrderDetail ON Product.Id = OrderDetail.ProductId
                                    INNER JOIN 'Order' OrderTable ON  OrderTable.Id = OrderDetail.OrderId
                                    INNER JOIN Supplier Supplier ON Supplier.Id = Product.SupplierId
                                    INNER JOIN Category Category ON Category.Id = Product.CategoryId
                                    INNER JOIN Employee Employee ON  Employee.Id = OrderTable.EmployeeId
                                    INNER JOIN EmployeeTerritory EmployeeTerritory ON EmployeeTerritory.EmployeeId = Employee.Id
                                    INNER JOIN Territory Territory ON Territory.Id = EmployeeTerritory.TerritoryId
                                    INNER JOIN Customer Customer ON Customer.Id = OrderTable.CustomerId
                                    WHERE Product.Id = @ProdId";


        private static readonly Lazy<string> LambdySqlTemplate = new (() =>
        {
            return LambdyQuery
                .ByModel<LargeJoinSqlModel>()
                .Raw.From(@"
                                    FROM Product Product
                                    INNER JOIN OrderDetail OrderDetail ON Product.Id = OrderDetail.ProductId
                                    INNER JOIN 'Order' OrderTable ON  OrderTable.Id = OrderDetail.OrderId
                                    INNER JOIN Supplier Supplier ON Supplier.Id = Product.SupplierId
                                    INNER JOIN Category Category ON Category.Id = Product.CategoryId
                                    INNER JOIN Employee Employee ON  Employee.Id = OrderTable.EmployeeId
                                    INNER JOIN EmployeeTerritory EmployeeTerritory ON EmployeeTerritory.EmployeeId = Employee.Id
                                    INNER JOIN Territory Territory ON Territory.Id = EmployeeTerritory.TerritoryId
                                    INNER JOIN Customer Customer ON Customer.Id = OrderTable.CustomerId")
                .Select(x => new LargeJoinQueryResult
                {
                    ProductId = x.Product.Id,
                    OrderDetailId = x.OrderDetail.Id,
                    OrderId = x.OrderTable.Id,
                    SupplierId = x.Supplier.Id,
                    CategoryId = x.Category.Id,
                    EmployeeId = x.Employee.Id,
                    EmployeeTerritoryId = x.EmployeeTerritory.Id,
                    TerritoryId = x.Territory.Id,
                    CustomerId = x.Customer.Id
                })
                .Compile(new LambdyCompilerOptions() {RemoveEmptyTokens = false})
                .Sql;
        });

        [Benchmark]
        public int LambdyAndDapperQuery()
        {
            var counter = 0;
            for (var prodId = 0; prodId < PassIterations; prodId++)
            {
                using var dbConnection = SqlLiteConnectionFactory.CreateConnection();
                dbConnection.Open();
                var id = prodId;
                
                var query = LambdyQuery
                    .ByModel<LargeJoinSqlModel>()
                    .WithTemplate(LambdySqlTemplate.Value)
                    .Where(x => x.Product.Id == id)
                    .Compile();
                
                var res = dbConnection
                    .Query<LargeJoinQueryResult>(query.Sql, query.Parameters);
                
                var list = res.ToList();
                counter += list.Count;
            }
            
            return counter;
        }
        
        
        [Benchmark()]
        public int DapperQuery()
        {
            var counter = 0;
            for (var prodId = 0; prodId < PassIterations; prodId++)
            {
                using var dbConnection = SqlLiteConnectionFactory.CreateConnection();
                dbConnection.Open();

                var dynParams = new Dictionary<string, object> {{"@ProdId", prodId}};

                var res = dbConnection
                    .Query<LargeJoinQueryResult>(DapperSql,dynParams);
                
                var list = res.ToList();
                counter += list.Count;
            }

            return counter;
        }
        
        
        [Benchmark(Baseline = true)]
        public int EfCoreQuery()
        {
            var counter = 0;
            for (var prodId = 0; prodId < PassIterations; prodId++)
            {
                var id = prodId;
                using var dbContext = new NorthwindContext();
                var query = (from product in dbContext.Products
                    join orderDetail in dbContext.OrderDetails
                        on product.Id equals orderDetail.ProductId
                    join order in dbContext.Orders
                        on orderDetail.OrderId equals order.Id
                    join supplier in dbContext.Suppliers
                        on product.SupplierId equals supplier.Id
                    join category in dbContext.Categories
                        on product.CategoryId equals category.Id
                    join employee in dbContext.Employees
                        on order.EmployeeId equals employee.Id
                    join employeeTerritory in dbContext.EmployeeTerritories
                        on employee.Id equals employeeTerritory.EmployeeId
                    join territory in dbContext.Territories
                        on employeeTerritory.TerritoryId equals territory.Id
                    join customer in dbContext.Customers
                        on order.CustomerId equals customer.Id
                    select new LargeJoinSqlModel
                    {
                        Product = product,
                        OrderTable = order,
                        OrderDetail = orderDetail,
                        Supplier = supplier,
                        Category = category,
                        Employee = employee,
                        EmployeeTerritory = employeeTerritory,
                        Territory = territory,
                        Customer = customer
                    }).Where(x => x.Product.Id == id)
                    .Select(x => new LargeJoinQueryResult
                    {
                        ProductId = x.Product.Id,
                        OrderDetailId = x.OrderDetail.Id,
                        OrderId = x.OrderTable.Id,
                        SupplierId = x.Supplier.Id,
                        CategoryId = x.Category.Id,
                        EmployeeId = x.Employee.Id,
                        EmployeeTerritoryId = x.EmployeeTerritory.Id,
                        TerritoryId = x.Territory.Id,
                        CustomerId = x.Customer.Id
                    });

                var result = query
                    .AsNoTracking()
                    .ToList();
                counter += result.Count;
            }
            
            return counter;
        }
    }
}