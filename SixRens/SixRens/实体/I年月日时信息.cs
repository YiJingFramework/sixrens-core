using YiJingFramework.StemsAndBranches;

namespace SixRens.实体
{
    public interface I年月日时信息
    {
        HeavenlyStem 年干 { get; }
        HeavenlyStem 月干 { get; }
        HeavenlyStem 日干 { get; }
        HeavenlyStem 时干 { get; }
        EarthlyBranch 年支 { get; }
        EarthlyBranch 月支 { get; }
        EarthlyBranch 日支 { get; }
        EarthlyBranch 时支 { get; }

        bool 昼占 { get; }
        EarthlyBranch 月将 { get; }
    }
}
