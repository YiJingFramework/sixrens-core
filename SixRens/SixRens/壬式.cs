using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;

namespace SixRens
{
    public sealed class 壬式
    {
        public I年月日时 年月日时 { get; }

        public 地支盘 地盘 { get; }
        public 地支盘 天盘 { get; }

        public I四课 四课 { get; }
        public I三传 三传 { get; }

        public I天将盘 天将盘 { get; }
        public IReadOnlyList<I神煞表> 神煞表 { get; }

        public 壬式(I年月日时 年月日时,
            I地盘插件 地盘插件,
            I天盘插件 天盘插件,
            I四课插件 四课插件,
            I三传插件 三传插件,
            I天将插件 天将插件,
            IEnumerable<I神煞插件> 神煞插件)
        {
            this.年月日时 = 年月日时;

            this.地盘 = 地盘插件.获取地盘(年月日时, new());
            this.天盘 = 天盘插件.获取天盘(年月日时, this.地盘);

            this.三传 = 三传插件.获取三传(年月日时, 地盘, 天盘);
            this.四课 = 四课插件.获取四课(年月日时, 地盘, 天盘, 三传);

            this.天将盘 = 天将插件.获取天将盘(年月日时, 地盘, 天盘, 三传, 四课);

            this.神煞表 = Array.AsReadOnly(
                神煞插件.Select(插件 => 插件.获取神煞表(年月日时, 地盘, 天盘, 三传, 四课, 天将盘))
                .ToArray());
        }
    }
}
