using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using YiJingFramework.StemsAndBranches;
using static SixRens.Api.I神煞插件;

namespace 测试用插件包.插件
{
    public class 神煞插件一 : I神煞插件, I神煞内容提供器
    {
        public string? 插件名 => "测试用神煞一（子丑）";

        public Guid 插件识别码 => new Guid("A38281CE-0245-4658-9E6E-15473F762DF1");

        private sealed record 神煞(
            string 神煞名,
            IReadOnlyList<EarthlyBranch> 所在神) : I神煞
        { }

        private static readonly 神煞[] 神煞表 = new[]
        {
            new 神煞("作子", new[] { new EarthlyBranch(1) }),
            new 神煞("作丑", new[] { new EarthlyBranch(2) }),
            new 神煞("组合煞", new[] { new EarthlyBranch(1), new EarthlyBranch(2) }),
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
