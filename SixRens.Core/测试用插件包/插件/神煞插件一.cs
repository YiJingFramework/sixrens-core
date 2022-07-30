using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 神煞插件一 : I神煞插件
    {
        public string? 插件名 => "测试用神煞一（子丑）";

        public Guid 插件识别码 => new Guid("A38281CE-0245-4658-9E6E-15473F762DF1");

        private sealed record 神煞(
            string 神煞名,
            IReadOnlyList<EarthlyBranch> 所在神) : I神煞题目, I神煞内容
        { }

        private static readonly 神煞[] 神煞表 = new[]
        {
            new 神煞("作子", new[] { new EarthlyBranch(1) }),
            new 神煞("作丑", new[] { new EarthlyBranch(2) }),
            new 神煞("组合煞", new[] { new EarthlyBranch(1), new EarthlyBranch(2) }),
        };
        public IEnumerable<I神煞题目> 支持的神煞 => 神煞表;

        public I神煞内容 获取神煞(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, string 神煞题目)
        {
            return 神煞表.Where(s => s.神煞名 == 神煞题目).Single();
        }
    }
}
