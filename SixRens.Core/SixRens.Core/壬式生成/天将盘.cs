using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 天将盘 : I天将盘
    {
        private readonly Dictionary<EarthlyBranch, 天将> 字典;
        internal 天将盘(Guid 所用插件, I天将盘 天将盘)
        {
            this.所用插件 = 所用插件;
            this.字典 = new(12);
            for (int 序数 = 1; 序数 <= 12; 序数++)
            {
                EarthlyBranch 地支 = new EarthlyBranch(序数);
                this.字典.Add(地支, 天将盘.取乘将(地支));
            }
        }
        public Guid 所用插件 { get; }
        public 天将 取乘将(EarthlyBranch 地支)
        {
            Debug.Assert(this.字典.ContainsKey(地支));
            return this.字典[地支];
        }
    }
}
