# Mango

[![Build Status](https://dev.azure.com/q932104843/Mango.Core/_apis/build/status/HahaMango.Core?branchName=master)](https://dev.azure.com/q932104843/Mango.Core/_build/latest?definitionId=7&branchName=master)

一个自用的基于`aspnet core`的轻量级webapi框架。

## Mango.Core

### 安装使用

Package Manager:

```powershell
Install-Package Mango.Core -Version 2.0.15
```

dotnet cli

```powershell
dotnet add package Mango.Core --version 2.0.15
```

### Mango.Core.ApiResponse

命名空间提供webapi的基础包装实体`ApiResult`：

```csharp
public class ApiResult
{
    public Code Code { get; set; }

    public string Message { get; set; }
}

public class ApiResult<T> : ApiResult
{
    public T Data { get; set; }
}
```

### Mango.Core.Authentication

命名空间授权服务，目前都是基于JWT的令牌颁发和认证。

在`Mango.Core.Authentication.Extension`中已经包装了常用的拓展方法可以在`Startup.cs`中进行简单的配置后即可支持JWT支持。

### Mango.Core.AutoMapper

命名空间提供对象映射的服务。为数据库实体和视图实体之间提供对象的映射。

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

使用方式如下，任何对象都可以调用`MapTo()`进行映射。

```csharp
//执行映射，targetObject类型为TObject
var targetObject = sourceObject.MapTo<TObject>();
```

### Mango.Core.Cache

该命名空间提供缓存的配置，包含基于内存的缓存（待完善），还有基于`redis`的分布式缓存。

配置：

```csharp
public class Startup
{
    //...
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMangoRedis(option => 
        {
            ConnectionString = "..."
        });
    }
    //...
}
```

配置可使用依赖注入方式在任意地址注入`ICache`来使用。

### Mango.Core.ControllerAbstractions

该命名空间提供基于`ApiResult`包装的基本`controller`抽象，提供各种方便的基于`ApiResult`的状态返回方法。

提供依赖于`Mango.Core.Authentication`的JWT授权认证用户信息获取方法。

### Mango.Core.Converter

该命名空间提供JSON类型转换器。JSON序列化包是`System.Text.Json`。

目前提供6种类型的转换器

- int
- int?
- long
- long?
- DateTime
- DateTime?

目前这些转换器可能因为`.netstandard 2.0`的原因还只能通过手动在`Startup.cs`中引入，暂无法封装。

### Mango.Core.DataStructure

命名空间提供基础的数据结构，例如通用分页请求参数，通用分页列表。

### Mango.Core.Encryption

命名空间提供目前仅支持`SHA256`的加密，以后将添加更多。

### Mango.Core.EntityFramework

该命名空间提供基于EF仓储的基础表结构，Id的生成采用雪花算法。

### Mango.Core.Enums

该命名空间提供基本的枚举。

### Mango.Core.Exception

命名空间提供基本的自定义异常类。

### Mango.Core.Extension

该命名空间归类其他的一些扩展类，例如一些分页扩展类，增删查改扩展类。

### Mango.Core.HttpService

命名空间提供HTTP服务的基本抽象，和实体。

### Mango.Core.KeyGenerator

命名空间包括键生成器接口，以及目前支持的雪花算法的接口实现。

### Mango.Core.Logger

命名空间提供日志创建，在没有依赖注入的时候也可以方便的使用日志。

### Mango.Core.Serialization

命名空间提供JSON的序列化。

无需配置，安装该包就可以使用

```csharp
var list = new List<int>
{
    1,
    2,
    3
};

var jsonList = list.ToJson();
```

## Mango.EntityFramework
