using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 天将盘 : I天将盘
    {
        internal 天将盘(Guid 所用插件, I去冗天将盘 天将盘)
        {
            this.所用插件 = 所用插件;
            this.为顺行 = 天将盘.为顺行;
            this.贵人天盘所乘 = 天将盘.贵人天盘所乘;
        }

        public Guid 所用插件 { get; }

        public bool 为顺行 { get; }
        public EarthlyBranch 贵人天盘所乘 { get; }

        public 天将 取乘将(EarthlyBranch 地支)
        {
            int 方向 = 为顺行 ? 1 : -1;
            var 差异 = 地支.Index - 贵人天盘所乘.Index;
            return (天将)(方向 * 差异);
        }

        public EarthlyBranch 取乘神(天将 天将)
        {
            int 方向 = 为顺行 ? 1 : -1;
            return 贵人天盘所乘.Next(方向 * (int)天将);
        }
    }
}
