﻿using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using SixRens.Tools;
using SixRens.Tools.十二长生扩展;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 四课 : I四课
    {
        internal 四课(I年月日时 年月日时, I天地盘 天地盘)
        {
            {
                var 日 = 年月日时.日干;
                var 日阳 = 天地盘.取乘神(日.寄宫());
                var 日阴 = 天地盘.取乘神(日阳);

                this.日 = 日;
                this.日阳 = 日阳;
                this.日阴 = 日阴;
            }

            {
                var 辰 = 年月日时.日支;
                var 辰阳 = 天地盘.取乘神(辰);
                var 辰阴 = 天地盘.取乘神(辰阳);

                this.辰 = 辰;
                this.辰阳 = 辰阳;
                this.辰阴 = 辰阴;
            }
        }

        public HeavenlyStem 日 { get; }
        public EarthlyBranch 日阳 { get; }
        public EarthlyBranch 日阴 { get; }
        public EarthlyBranch 辰 { get; }
        public EarthlyBranch 辰阳 { get; }
        public EarthlyBranch 辰阴 { get; }
    }
}
