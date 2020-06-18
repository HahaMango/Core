using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Core.Network
{
    public class HandleBusinessTcpServer<T> : HandlePackageLineTcpServer<T>
        where T : DataPackage, new()
    {
        protected override async ValueTask<Memory<byte>> HandleBusiness(ReadOnlyMemory<byte> input)
        {
            var s = Encoding.UTF8.GetString(input.ToArray());
            s += new Random().Next();
            var r = Encoding.UTF8.GetBytes(s);
            return await Task.FromResult(r);
        }
    }
}
