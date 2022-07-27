using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace 测试用插件包.插件
{
    public class 课体插件二 : I课体插件
    {
        public string? 插件名 => "测试用课体二（体二）";

        public Guid 插件识别码 => new Guid("EA09B4A8-EBC3-4CCC-91B8-C76F7304F23E");

        private sealed record 课体(
            string 课体名) : I课体
        { }

        public IEnumerable<I课体> 支持的课体
        {
            get
            {
                yield return new 课体("体二一");
                yield return new 课体("体二二");
                yield return new 课体("体同");
            }
        }

        public IEnumerable<I课体> 识别课体(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, I年命? 课主年命, IReadOnlyList<I年命> 对象年命, IReadOnlyList<I神煞> 神煞列表)
        {
            yield return new 课体("体二二");
            yield return new 课体("体同");
        }
    }
}
