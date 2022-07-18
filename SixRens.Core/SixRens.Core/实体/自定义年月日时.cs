using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体
{
    public sealed record 自定义年月日时(
        HeavenlyStem 年干,
        EarthlyBranch 年支,
        HeavenlyStem 月干,
        EarthlyBranch 月支,
        HeavenlyStem 日干,
        EarthlyBranch 日支,
        HeavenlyStem 时干,
        EarthlyBranch 时支,
        bool 昼占,
        EarthlyBranch 月将) : I年月日时信息
    { }
}
