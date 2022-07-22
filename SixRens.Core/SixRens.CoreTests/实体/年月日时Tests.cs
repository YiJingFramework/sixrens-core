using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixRens.Core.实体.年月日时;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体.Tests
{
    [TestClass()]
    public class 年月日时Tests
    {
        [TestMethod()]
        public void 月干时干Test()
        {
            var 年干 = new HeavenlyStem(1);
            var 月干 = new HeavenlyStem(3);
            var 日干 = new HeavenlyStem(1);
            var 时干 = new HeavenlyStem(1);

            var 年支 = new EarthlyBranch(1);
            var 月支 = new EarthlyBranch(3);
            var 日支 = new EarthlyBranch(1);
            var 时支 = new EarthlyBranch(1);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    var 获得时干 = new 可检验年月日时(年干, 年支, 月支, 日干, 日支, 时支, false, 年支).时干;
                    Assert.AreEqual(获得时干, 时干);
                    时干 = 时干.Next();
                    时支 = 时支.Next();
                }
                日干 = 日干.Next();
                日支 = 日支.Next();
            }

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    var 获得月干 = new 可检验年月日时(年干, 年支, 月支, 日干, 日支, 时支, false, 年支).月干;
                    Assert.AreEqual(获得月干, 月干);
                    月干 = 月干.Next();
                    月支 = 月支.Next();
                }
                年干 = 年干.Next();
                年支 = 年支.Next();
            }
        }
    }
}