using SixRens.Api.工具;
using YiJingFramework.StemsAndBranches;

namespace SixRens.实体
{
    public sealed record 可检验年月日时(
        HeavenlyStem 年干,
        EarthlyBranch 年支,
        EarthlyBranch 月支,
        HeavenlyStem 日干,
        EarthlyBranch 日支,
        EarthlyBranch 时支,
        bool 昼占,
        EarthlyBranch 月将) : I年月日时信息
    {
        public HeavenlyStem 月干
        {
            get
            {
                var 首月天干 = (年干.Index % 5) * 2 + 1;
                var 月偏移 = (月支.Index + 9) % 12;
                return new(首月天干 + 月偏移);
            }
        }

        public HeavenlyStem 时干
            => new HeavenlyStem(((日干.Index + 4) % 5) * 2 + 时支.Index);

        public bool 检验昼夜()
        {
            return this.昼占 == (this.时支.Index is >= 4 and < 10);
        }
        public bool 检验年日阴阳()
        {
            return 年干.阴阳() == 年支.阴阳()
                && 日干.阴阳() == 日支.阴阳();
        }
    }
}
