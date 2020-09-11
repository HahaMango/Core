using Mango.Core.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;

namespace Mango.Core.Test
{
    /// <summary>
    /// 测试抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseTestStartup<T>
        where T : DbContext,new()
    {
        /// <summary>
        /// 输出
        /// </summary>
        protected readonly ITestOutputHelper _output;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="testOutputHelper"></param>
        public BaseTestStartup(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
            _output.WriteLine("初始化");
        }

        /// <summary>
        /// 初始化测试环境
        /// </summary>
        /// <returns></returns>
        public virtual IServiceProvider InitTestEnv()
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(DateTime.Now.Ticks.ToString())
                .Options;
            var type = typeof(T);
            var dbcontext = (T)Activator.CreateInstance(type, options);

            var serviceCollection = new ServiceCollection();
            #region 添加DI
            serviceCollection.AddAutoMapper();
            #endregion

            #region mock对象
            InitMock(dbcontext, serviceCollection);
            #endregion

            return serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// 初始化mock对象
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="services"></param>
        public abstract void InitMock(T dbContext, IServiceCollection services);

        /// <summary>
        /// 获取mock的logger
        /// </summary>
        /// <typeparam name="TargetService"></typeparam>
        /// <returns></returns>
        public IMock<ILogger<TargetService>> GetMockLogger<TargetService>()
        {
            var log = new Mock<ILogger<TargetService>>();
            log.Setup(x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<System.Exception>(),
                    (Func<It.IsAnyType, System.Exception, string>)It.IsAny<object>()))
                    .Callback(new InvocationAction(invocation =>
                    {
                        var logLevel = (LogLevel)invocation.Arguments[0];
                        var eventId = (EventId)invocation.Arguments[1];
                        var state = invocation.Arguments[2];
                        var exception = (System.Exception?)invocation.Arguments[3];
                        var formatter = invocation.Arguments[4];

                        var invokeMethod = formatter.GetType().GetMethod("Invoke");
                        var logMessage = (string?)invokeMethod?.Invoke(formatter, new[] { state, exception });

                        _output.WriteLine(logMessage);
                    }));

            return log;
        }
    }
}
