using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using YiJingFramework.StemsAndBranches;
using static SixRens.Api.I神煞插件;

namespace 测试用插件包.插件
{
    public class 神煞插件二 : I神煞插件, I神煞内容提供器
    {
        public string? 插件名 => "测试用神煞二（寅卯）";

        public Guid 插件识别码 => new Guid("2DA7F35D-88F5-40FB-9135-83D38DFB7153");

        private sealed record 神煞(
            string 神煞名,
            IReadOnlyList<EarthlyBranch> 所在神) : I神煞
        { }

        private static readonly 神煞[] 神煞表 = new[]
        {
            new 神煞("组合煞", new[] { new EarthlyBranch(3), new EarthlyBranch(4) }),
            new 神煞("作寅", new[] { new EarthlyBranch(3) }),
            new 神煞("作卯", new[] { new EarthlyBranch(4) }),
        };
        public IEnumerable<string> 支持神煞的名称 => 神煞表.Select(t => t.神煞名);

        public IEnumerable<EarthlyBranch> 取所在神(string 神煞名)
        {
            return 神煞表.Where(s => s.神煞名 == 神煞名).Single().所在神;
        }

        public I神煞内容提供器 获取神煞内容提供器(I起课信息 起课信息, I天地盘 天地盘, I四课 四课, I三传 三传, I天将盘 天将盘)
        {
            return this;
        }
    }
}
