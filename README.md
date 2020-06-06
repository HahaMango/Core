# Mango 基础设施

[![Build Status](https://dev.azure.com/q932104843/Mango.Core/_apis/build/status/HahaMango.Core?branchName=master)](https://dev.azure.com/q932104843/Mango.Core/_build/latest?definitionId=7&branchName=master)

## Mango.Core 包含常用核心组件

### 添加AutoMapper支持

```csharp
public class Startup
{
    //...
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper();
    }
    //...
}
```

### How To Use

```csharp
//执行映射，targetObject类型为TObject
var targetObject = sourceObject.MapTo<TObject>();
```

## Mango.EntityFramework 包含仓储层基础组件
