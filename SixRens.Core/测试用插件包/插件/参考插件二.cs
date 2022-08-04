using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 参考插件二 : I参考插件
    {
        public string? 插件名 => "测试用参考二";

        public Guid 插件识别码 => new Guid("95413F75-39C1-44A0-9C7C-65863EBFC4E8");

        private sealed record 参考(
            string 题目,
            string 内容,
            EarthlyBranch? 相关宫位) : I占断参考
        { }

        public IEnumerable<I占断参考> 生成占断参考(I起课信息 起课信息, I天地盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, IReadOnlyList<I神煞> 神煞列表, IReadOnlyList<I课体> 课体列表)
        {
            yield return new 参考("二个参考", "全课的参考二", null);
            yield return new 参考("二个参考", "丑宫的参考二", new(2));
            yield return new 参考("统一参考", "统一的参考二", null);
        }
    }
}
