using com.nlf.calendar;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体.年月日时
{
    public sealed class 真实年月日时 : I年月日时信息
    {
        public HeavenlyStem 年干 { get; }
        public HeavenlyStem 月干 { get; }
        public HeavenlyStem 日干 { get; }
        public HeavenlyStem 时干 { get; }
        public EarthlyBranch 年支 { get; }
        public EarthlyBranch 月支 { get; }
        public EarthlyBranch 日支 { get; }
        public EarthlyBranch 时支 { get; }
        public bool 昼占 { get; }
        public EarthlyBranch 月将 { get; }

        public 真实年月日时(DateTime 日期)
            : this(Lunar.fromDate(日期)) { }

        public 真实年月日时(Lunar 日期)
        {
            var 八字 = 日期.getEightChar();
            八字.setSect(1);

            年干 = 天干表[八字.getYearGan()];
            月干 = 天干表[八字.getMonthGan()];
            日干 = 天干表[八字.getDayGan()];
            时干 = 天干表[八字.getTimeGan()];

            年支 = 地支表[八字.getYearZhi()];
            月支 = 地支表[八字.getMonthZhi()];
            日支 = 地支表[八字.getDayZhi()];
            时支 = 地支表[八字.getTimeZhi()];

            昼占 = 时支.Index is >= 4 and < 10;

            月将 = new EarthlyBranch(
                日期.getPrevQi().getName() switch
                {
                    "雨水" => 12,
                    "春分" => 11,
                    "谷雨" => 10,
                    "小满" => 9,
                    "夏至" => 8,
                    "大暑" => 7,
                    "处暑" => 6,
                    "秋分" => 5,
                    "霜降" => 4,
                    "小雪" => 3,
                    "冬至" => 2,
                    _ => 1 // "大寒"
                });
        }

        private static IReadOnlyDictionary<string, HeavenlyStem> 天干表 { get; }
            = HeavenlyStem.BuildStringStemTable("C").ToDictionary(item => item.s, item => item.stem);
        private static IReadOnlyDictionary<string, EarthlyBranch> 地支表 { get; }
            = EarthlyBranch.BuildStringBranchTable("C").ToDictionary(item => item.s, item => item.branch);
    }
}
