using SixRens.Api.实体;
using SixRens.Core.实体;
using SixRens.Core.实体.年月日时;
using YiJingFramework.StemsAndBranches;
using static SixRens.Api.实体.I年月日时;

namespace SixRens.Core.工具.年月日时
{
    public static class 年月日时扩展
    {
        private sealed class 自由年月日时 : I年月日时, I年月日时信息
        {
            private sealed class 旬 : I旬
            {
                public HeavenlyStem 旬首干 => new HeavenlyStem(1);
                public 旬((HeavenlyStem 干, EarthlyBranch 支) 旬内一日)
                {
                    旬首支 = new EarthlyBranch(旬内一日.支.Index - 旬内一日.干.Index + 1);
                }
                public EarthlyBranch 旬首支 { get; }
                public EarthlyBranch 空亡一 => new EarthlyBranch(旬首支.Index - 2);
                public EarthlyBranch 空亡二 => new EarthlyBranch(旬首支.Index - 1);
                public IEnumerable<(HeavenlyStem 干, EarthlyBranch 支)> 每日干支
                {
                    get
                    {
                        HeavenlyStem 干 = 旬首干;
                        EarthlyBranch 支 = 旬首支;
                        for (; 干.Index <= 10;)
                        {
                            yield return (干, 支);
                            干 = new HeavenlyStem(干.Index + 1);
                            支 = new EarthlyBranch(支.Index + 1);
                        }
                    }
                }

                public HeavenlyStem? 获取对应天干(EarthlyBranch 地支)
                {
                    if (地支 == 空亡一 || 地支 == 空亡二)
                        return null;
                    return new HeavenlyStem((地支.Index - 旬首支.Index + 13) % 12);
                }
                public EarthlyBranch 获取对应地支(HeavenlyStem 天干)
                {
                    return new EarthlyBranch(天干.Index - 1 + 旬首支.Index);
                }
            }

            public 自由年月日时(
                HeavenlyStem 年干,
                EarthlyBranch 年支,
                HeavenlyStem 月干,
                EarthlyBranch 月支,
                HeavenlyStem 日干,
                EarthlyBranch 日支,
                HeavenlyStem 时干,
                EarthlyBranch 时支,
                bool 昼占,
                EarthlyBranch 月将)
            {
                this.年干 = 年干;
                this.月干 = 月干;
                this.日干 = 日干;
                this.时干 = 时干;
                this.年支 = 年支;
                this.月支 = 月支;
                this.日支 = 日支;
                this.时支 = 时支;
                this.昼占 = 昼占;
                旬所在 = new 旬((日干, 日支));
                this.月将 = 月将;
            }

            public HeavenlyStem 年干 { get; }

            public HeavenlyStem 月干 { get; }

            public HeavenlyStem 日干 { get; }

            public HeavenlyStem 时干 { get; }

            public EarthlyBranch 年支 { get; }

            public EarthlyBranch 月支 { get; }

            public EarthlyBranch 日支 { get; }

            public EarthlyBranch 时支 { get; }

            public bool 昼占 { get; }

            public I旬 旬所在 { get; }

            public EarthlyBranch 月将 { get; }
        }

        public static I年月日时 生成年月日时(this I年月日时信息 年月日时信息)
        {
            return new 自由年月日时(
                年月日时信息.年干, 年月日时信息.年支, 年月日时信息.月干, 年月日时信息.月支,
                年月日时信息.日干, 年月日时信息.日支, 年月日时信息.时干, 年月日时信息.时支,
                年月日时信息.昼占, 年月日时信息.月将);
        }

        public static I年月日时信息 修改信息(this I年月日时信息 年月日时信息,
            EarthlyBranch? 月将 = null, bool? 昼占 = null)
        {
            return new 自由年月日时(
                年月日时信息.年干, 年月日时信息.年支, 年月日时信息.月干, 年月日时信息.月支,
                年月日时信息.日干, 年月日时信息.日支, 年月日时信息.时干, 年月日时信息.时支,
                昼占 ?? 年月日时信息.昼占, 月将 ?? 年月日时信息.月将);
        }

        public static bool 检验八字(this I年月日时信息 年月日时信息, bool 检验昼夜 = false)
        {
            var 月时正确年月日时 = new 可检验年月日时(
                年月日时信息.年干, 年月日时信息.年支,
                年月日时信息.月支,
                年月日时信息.日干, 年月日时信息.日支,
                年月日时信息.时支,
                年月日时信息.昼占, 年月日时信息.月将);
            if (!月时正确年月日时.检验年日阴阳())
                return false;
            if (月时正确年月日时.月干 != 年月日时信息.月干 ||
                月时正确年月日时.时干 != 年月日时信息.时干)
                return false;
            if (检验昼夜 && !月时正确年月日时.检验昼夜())
                return false;
            return true;
        }
    }
}
