###To scaffold EF core context
```
dotnet tool install --global dotnet-ef
```
```
dotnet-ef dbcontext scaffold "DataSource=DataSource/Northwind_large.sqlite" Microsoft.EntityFrameworkCore.Sqlite --output-dir EfCoreContext --context "NorthwindContext"
```

###For benchmarking documentation see
https://github.com/dotnet/benchmarkdotnet

https://benchmarkdotnet.org/
