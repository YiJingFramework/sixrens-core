using SixRens.Api.实体;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.名称转换
{
    public static class 名称扩展
    {
        public static 名称表<天将> 天将简称表 { get; } = new()
        {
            { 天将.贵人, "贵" },
            { 天将.螣蛇, "蛇" },
            { 天将.朱雀, "雀" },
            { 天将.六合, "合" },
            { 天将.勾陈, "勾" },
            { 天将.青龙, "龙" },
            { 天将.天空, "空" },
            { 天将.白虎, "虎" },
            { 天将.太常, "常" },
            { 天将.玄武, "玄" },
            { 天将.太阴, "阴" },
            { 天将.天后, "后" }
        };

        public static string 简称(this 天将 天将)
        {
            if (天将简称表.TryGetValue(天将, out var 结果))
                return 结果;
            return 天将.ToString();
        }

        public static 名称表<EarthlyBranch> 天神名称表 { get; } = new()
        {
            { new EarthlyBranch(1), "神后" },
            { new EarthlyBranch(2), "大吉" },
            { new EarthlyBranch(3), "功曹" },
            { new EarthlyBranch(4), "太冲" },
            { new EarthlyBranch(5), "天罡" },
            { new EarthlyBranch(6), "太乙" },
            { new EarthlyBranch(7), "胜光" },
            { new EarthlyBranch(8), "小吉" },
            { new EarthlyBranch(9), "传送" },
            { new EarthlyBranch(10), "从魁" },
            { new EarthlyBranch(11), "河魁" },
            { new EarthlyBranch(12), "登明" }
        };

        public static string 天神名(this EarthlyBranch 天神)
        {
            if (天神名称表.TryGetValue(天神, out var 结果))
                return 结果;
            return 天神.ToString("C");
        }

        public static string 地支名(this EarthlyBranch 地支)
        {
            return 地支.ToString("C");
        }
    }
}
