using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 天地盘 : I天地盘
    {
        private readonly int 偏移;

        public 天地盘(I年月日时 年月日时)
        {
            this.偏移 = 年月日时.月将.Index - 年月日时.时支.Index;
        }

        public EarthlyBranch 取临地(EarthlyBranch 天盘支)
        {
            return 天盘支.Next(-this.偏移);
        }

        public EarthlyBranch 取乘神(EarthlyBranch 地盘支)
        {
            return 地盘支.Next(this.偏移);
        }
    }
}
