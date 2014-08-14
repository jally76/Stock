using StooqProxy.Logic.Consts;
using StooqProxy.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StooqProxyTest
{
    public class TBasicExploration
    {
        [Fact]
        public void StooqWebsiteIsOnLine()
        {
            Assert.NotEmpty(HttpHelper.GetWeb(StooqWebConsts.Address));
        }

    }
}
