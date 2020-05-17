using Mango.Core.Serialization.Extension;
using MangoTest.TestData;
using System;
using System.Collections.Generic;
using Xunit;

namespace MangoTest
{
    public class JsonTest
    {
        [Theory]
        [MemberData(nameof(JsonTestData.MyTestData),MemberType = typeof(JsonTestData))]
        public void Test(object source,string result)
        {
            var json = source.ToJsonUtf8();
            Assert.Equal(result, json);
        }
    }
}
