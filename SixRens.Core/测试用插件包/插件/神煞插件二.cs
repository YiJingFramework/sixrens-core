using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 神煞插件二 : I神煞插件
    {
        public string? 插件名 => "测试用神煞二（寅卯）";

        public Guid 插件识别码 => new Guid("2DA7F35D-88F5-40FB-9135-83D38DFB7153");

        private sealed record 神煞(
            string 神煞名,
            IReadOnlyList<EarthlyBranch> 所在神) : I神煞题目, I神煞内容
        { }

        private static readonly 神煞[] 神煞表 = new[]
        {
            new 神煞("组合煞", new[] { new EarthlyBranch(3), new EarthlyBranch(4) }),
            new 神煞("作寅", new[] { new EarthlyBranch(3) }),
            new 神煞("作卯", new[] { new EarthlyBranch(4) }),
        };
        public IEnumerable<I神煞题目> 支持的神煞 => 神煞表;

        public I神煞内容 获取神煞(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, I神煞题目 神煞题目)
        {
            return 神煞表.Where(s => s.神煞名 == 神煞题目.神煞名).Single();
        }
    }
}
