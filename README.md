# Lambdy
![build](https://github.com/vladimirbelyayev/Lambdy/actions/workflows/build-main.yml/badge.svg)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/lambdy?label=Nuget)](https://www.nuget.org/packages/Lambdy)

Library for building SQL from Lambda expressions, for use in conjunction with Micro-ORMs


## Basic usage example
```c#
// With model

var joins = $"FROM {nameof(Person)} Table1 " +
            $"JOIN {nameof(Address)} Table2 ON Table2.Id = Table1.AddressId ";

var lambdyResult = LambdyQuery
	.ByModel<JoinsModel>()
	.WithTemplate($"{LambdyTemplateTokens.Select}" +
            $"{joins}" +
            $"{LambdyTemplateTokens.Where}" +
            $"{LambdyTemplateTokens.OrderBy}" +
            $"{LambdyTemplateTokens.SkipTake}")
	.Where(x => x.Table1.Id == 1)
	.OrderBy(x => x.Table1.Col1)
	.ThenBy(x => x.Table1.Col2)
	.Skip(0)
	.Take(10)
	.Select(x => new SelectModel { AliasCol = x.Table1.Id })
	.Compile();
  
var parameterizedSql = lambdyResult.Sql;
var extendableParamDictionary = lambdyResult.Parameters;
```

```c#
// With anonymous type

var joins = $"FROM {nameof(Person)} Table1 " +
            $"JOIN {nameof(Address)} Table2 ON Table2.Id = Table1.AddressId ";
            
var lambdyResult = LambdyQuery
	.ByModel(new { 
            Table1 = (Person) null, 
            Table2 = (Address) null 
	})
	.WithTemplate($"{LambdyTemplateTokens.Select}" +
            $"{joins}" +
            $"{LambdyTemplateTokens.Where} AND Table1.Field2 != @AdditionalFilterParam " +
            $"{LambdyTemplateTokens.OrderBy}" +
            $"{LambdyTemplateTokens.SkipTake}")
	.Where(x => x.Table1.Id == 1)
	.OrderBy(x => x.Table1.Col1)
	.ThenBy(x => x.Table1.Col2)
	.Skip(0)
	.Take(10)
	.Select(x => new { AliasCol = x.Table1.Id })
	.Compile();
  
  
var parameterizedSql = lambdyResult.Sql;
var extendableParamDictionary = lambdyResult.Parameters;
extendableParamDictionary.Add("@AdditionalFilterParam", customValue);
```

```c#
// Use default template and raw from/join for simpler queries
            
var lambdyResult = LambdyQuery
	.ByModel(new { 
            Table1 = (Person) null, 
            Table2 = (Address) null 
	})
	.Raw.From($"FROM {nameof(Person)} Table1")
	.Raw.Join($"JOIN {nameof(Address)} Table2 ON Table2.Id = Table1.AddressId")
	.Where(x => x.Table1.Id == 1)
	.OrderBy(x => x.Table1.Col1)
	.ThenBy(x => x.Table1.Col2)
	.Skip(0)
	.Take(10)
	.Select(x => new { AliasCol = x.Table1.Id })
	.Compile();
  
  
var parameterizedSql = lambdyResult.Sql;
var extendableParamDictionary = lambdyResult.Parameters;
```

```c#
// Switch between using expressions and raw SQL
            
var lambdyResult = LambdyQuery
	.ByModel(new { 
            Table1 = (Person) null, 
            Table2 = (Address) null 
	})
	.Raw.From($"FROM {nameof(Person)} Table1")
	.Raw.Join($"JOIN {nameof(Address)} Table2 ON Table2.Id = Table1.AddressId")
	.Where(x => x.Table1.Id == 1)
	.Raw.Where("Table1.Id == @MyParam", new { MyParam = 1 })
	.Raw.OrderBy("ORDER BY CASE " + 
	"WHEN Table1.Col1 < @OrderParam THEN 1 " + 
	"ELSE 0 END", new { OrderParam = 5 })
	.ThenBy(x => x.Table1.Col2)
	.Skip(0)
	.Take(10)
	.Select(x => new { AliasCol = x.Table1.Id })
	.Compile();
  
  
var parameterizedSql = lambdyResult.Sql;
var extendableParamDictionary = lambdyResult.Parameters;
```
