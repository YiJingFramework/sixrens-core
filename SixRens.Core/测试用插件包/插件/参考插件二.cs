using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace 测试用插件包.插件
{
    public class 参考插件二 : I参考插件
    {
        public string? 插件名 => "测试用参考二";

        public Guid 插件识别码 => new Guid("95413F75-39C1-44A0-9C7C-65863EBFC4E8");

        private sealed record 参考(
            string 题目,
            string? 内容) : I占断参考题目, I占断参考内容
        { }

        private static readonly 参考[] 参考表 = new[]
        {
            new 参考("二个参考", "二个参考"),
            new 参考("回声参考", "回声参考"),
            new 参考("没有参考", null),
        };

        public IEnumerable<I占断参考题目> 支持的占断参考 => 参考表;

        public I占断参考内容 生成占断参考(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, IReadOnlyList<I神煞> 神煞列表, IReadOnlyList<I课体> 课体列表, string 占断参考题目)
        {
            return 参考表.Where(s => s.题目 == 占断参考题目).Single();
        }
    }
}
