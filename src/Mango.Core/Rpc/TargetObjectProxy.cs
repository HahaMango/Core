using Castle.DynamicProxy;
using Mango.Core.Rpc.Abstractions;
using Mango.Core.Rpc.Abstractions.DataStructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Rpc
{
    /// <summary>
    /// 目标对象代理对象
    /// </summary>
    internal class TargetObjectProxy : IAsyncInterceptor
    {
        private readonly IRpcClient _rpcClient;

        public TargetObjectProxy(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        /// <summary>
        /// 异步Task拦截器
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = ProfermAsync(invocation);
        }

        /// <summary>
        /// 有返回值异步Task拦截器
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 同步拦截器
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

        private async Task ProfermAsync(IInvocation invocation)
        {
            var targetType = invocation.TargetType.IsInterface ? 
                invocation.TargetType : invocation.TargetType.GetInterfaces()[0];
            var request = new MethodRpcRequest
            {
                TargetType = targetType,
                TargetMethod = invocation.MethodInvocationTarget,
                Params = invocation.Arguments
            };
            var response = await _rpcClient.InvokeMethodAsync(request);
            if(response.Status == -1)
            {
                if(response.Exception != null)
                {
                    throw response.Exception;
                }
                else
                {
                    throw new System.Exception("RPC调用异常");
                }
            }
            invocation.ReturnValue = response.ReturnData;
        }
    }
}
