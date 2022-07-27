using SixRens.Api.实体.壬式;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 地盘 : I地盘
    {
        private readonly Dictionary<EarthlyBranch, EarthlyBranch> 字典;
        internal 地盘(Guid 所用插件, I地盘 地盘)
        {
            this.所用插件 = 所用插件;
            this.字典 = new(12);
            for (int 序数 = 1; 序数 <= 12; 序数++)
            {
                EarthlyBranch 地支 = new EarthlyBranch(序数);
                this.字典.Add(地支, 地盘.取地支(地支));
            }
        }

        public Guid 所用插件 { get; }

        public EarthlyBranch 取地支(EarthlyBranch 位置)
        {
            Debug.Assert(this.字典.ContainsKey(位置));
            return this.字典[位置];
        }
    }
}
