using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.OrderBy
{
    public class OrderByTest
    {
        [Fact]
        public void OrderByShouldWork()
        {
            var expectedResult = $"{SqlClauses.OrderBy} TableOne.Id ASC";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .OrderBy(x => x.TableOne.Id)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void OrderByChainedShouldWork()
        {
            var expectedResult = $"{SqlClauses.OrderBy} TableOne.Id ASC, " +
                                 $"T.AddressLine2 ASC";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .OrderBy(x => x.TableOne.Id)
                .ThenBy(x => x.T.AddressLine2)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void OrderByDescendingShouldWork()
        {
            var expectedResult = $"{SqlClauses.OrderBy} TableOne.Id DESC";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .OrderByDescending(x => x.TableOne.Id)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void OrderByDescendingChainedShouldWork()
        {
            var expectedResult = $"{SqlClauses.OrderBy} TableOne.Id DESC, " +
                                 $"T.AddressLine2 DESC";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .OrderByDescending(x => x.TableOne.Id)
                .ThenByDescending(x => x.T.AddressLine2)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void OrderByMixedChainedShouldWork()
        {
            var expectedResult = $"{SqlClauses.OrderBy} TableOne.Id ASC, " +
                                 $"T.AddressLine2 DESC, " +
                                 $"Table3.PersonId ASC, " +
                                 $"TableOne.Age DESC";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .OrderBy(x => x.TableOne.Id)
                .ThenByDescending(x => x.T.AddressLine2)
                .ThenBy(x => x.Table3.PersonId)
                .ThenByDescending(x => x.TableOne.Age)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
    }
}