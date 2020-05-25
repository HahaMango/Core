using Mango.Core.Authentication.Jwt;
using Mango.Core.EntityFramework.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MangoTest
{
    public class JwtTest
    {
        [Fact]
        public void Gjwt()
        {
        }

        public class TestUser : SnowFlakeEntity
        {
            public TestUser() { }

            public TestUser(bool set)
            {
                base.SetId();
            }

            public string UserName { get; set; } = "mango";
        }
    }
}
