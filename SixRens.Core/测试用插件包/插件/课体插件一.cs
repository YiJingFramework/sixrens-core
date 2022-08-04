using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using static SixRens.Api.I课体插件;

namespace 测试用插件包.插件
{
    public class 课体插件一 : I课体插件, I课体内容提供器
    {
        public string? 插件名 => "测试用课体一（体一）";

        public Guid 插件识别码 => new Guid("9FCF02E3-1000-4891-8C86-51D00D157181");

        private sealed record 课体(
            string 课体名,
            bool 属此课体) : I课体
        { }

        private static readonly 课体[] 课体表 = new[]
        {
            new 课体("体一一", true),
            new 课体("体一二", false),
            new 课体("体同", true)
        };

        public IEnumerable<string> 支持的课体 => 课体表.Select(t => t.课体名);

        public bool 属此课体(string 课体名)
        {
            return 课体表.Where(k => k.课体名 == 课体名).Single().属此课体;
        }

        public I课体内容提供器 获取课体内容提供器(I起课信息 起课信息, I天地盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, IReadOnlyList<I神煞> 神煞列表)
        {
            return this;
        }
    }
}
