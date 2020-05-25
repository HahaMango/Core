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
            var config = new MangoJwtOptions
            {
                Key = @"MIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDJQJY/vIlQV8Hm
y41w6zSJRL7zo/57uj0T6OrJXgzo9pk5mtCWKW/+KTmQ7OCZVaIXKq+5OW1ibkjI
QDJFRBCbUoUN8799GiadCQJDzpcaGh5ev3wjxzqG8yR0bkkgMNkoYhX7IwJ+6B+1
zYsyQeVvhztyO2Rj79lId7oSO4Ku2qDomNe1LaBQP6rESKwdx6TIWPUr4CdbnLGX
AmJ95P66RQ8mqfeHbqvlbZhK/eUF0kNNxbCV0qH4lTFSugSMQaKRNg0H1tlc86dp
lyZpvf8KEekmNYwY1TZSTC4HwVzDiUn6+yh5EbkGlcC7tO1ClntGHwBGjukLKBk5
sjx5BgT/AgMBAAECggEBAK7qvOw+sNYswDIZnyCSvYHFR3Y5hhkBwM5KNa65WN0u
X/TKUxsAfv9X01ncGEYNQKmEB2Ekwaa4lfe+nDLJuulU6qI6xac1EHSSfO50Y65j
HxxYr8vlAECEmZ28sUASVNwdjF9PiX7Fv7HjKWWQEptB3XAmoNWfhKnQrd/k62uO
u4+n3Uej/cH+l1RpQQDqy15ojSIBwBDpZFVPJOt/oQpBgF0V/3NxJSiwAnJCWau7
vGeqFQPlNKW0AldE0Kv3l67FTXC97nnpWdihIvBM0sc8FLNGGg6B4GLTct+Yf5R0
mPI2KHL3o7TSburKcA03AQ+c2EQ3yh9edgRDa7G4pIECgYEA+yjIWIRU20TGDx/R
5ox0bdlZyeSQggwuS8rY2k6sU2hFa513WFB5R6duv47QVKEwGKlnccGrtvU2vNQF
fHYyB9odpkMCQphSUEKy2UrWEB4LJmekUFP4+AjXO0JOEsrQ88hXeSnpC2UXuZe5
fWn+udeUYrEFondCilBomJzA6r8CgYEAzSGQSXokpAec5Cvm0mTGIM1/H7nG7Jm2
BbJ5oyyieebGi35GY9RYrCpOIUkP01cBQaRhIv7ftlu6f4lEH/VpxCIRnA7AROsl
rkdQVAkWU99szd2/6zcAoAs6vU1n5SWF4cF6E/xMCney+x8tGYMWozRwha27uip+
YqJ4DY1ktcECgYEAmuE1WtCP+39Xm7AFomRuz+a+peZfUejnkvxnKWqWYiL
Nhy6DWPEub/53JZhsHOW5OGHYJWqoZslnvDMPWdV7VdZJ3QDHpdi7vhlNR8xxQcY
nqiJ2XqqL1LeDlyfqhWbS456tZornTlhG2OnvzafvJRpYxykHeMj/Sh9FsUCgYAy
kLJ7mlNL5+CB0lycwmCgl2ddz7K8ggt/jgYz9f27JOsOWbtKQn71OZx20gbHpuvV
XYrgUIme7y+i3phfdGR1B5zlpjE5C+oG8udXP8I0PKAagy4a8j0CNqJtJZaVwtEk
3EeWg5vO/MCu7Hl2j3zWEEgoe7IJ6w2qjLghRxhrQQKBgQCiAnVt7AAIokPYBF9X
wLvUq9MZvk+kbRmRulIKKDSjRAGlxPK3wG416xUfEEox/wmuQicWsb83SOqrSX6Z
vFixHmPWpvv8a0r26gmdHyhMmXTcl2P5RHdJ/iCBKj9/pIAabhenN/vClnsB8vv/
tN9fcep4jGpay5xZ0Nj2fSWygw==",
                Audience = "test",
                Issuer = "www.hahamango.cn",
                ExpiresSec = 3600
            };
            var jwt = new MangoJwtTokenHandler(config).IssuedToken(new TestUser(true));
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
