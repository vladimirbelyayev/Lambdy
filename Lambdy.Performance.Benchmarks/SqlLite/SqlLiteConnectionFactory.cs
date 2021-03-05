using System.Data.SQLite;

namespace Lambdy.Performance.Benchmarks.SqlLite
{
    public static class SqlLiteConnectionFactory
    {
        private static string DbFile = "DataSource/Northwind_large.sqlite";

        public static SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }
    }
}