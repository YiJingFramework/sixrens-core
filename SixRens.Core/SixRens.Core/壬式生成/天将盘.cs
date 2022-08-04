using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using System.Diagnostics;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 天将盘 : I天将盘
    {
        private readonly 天地盘 天地盘;
        private readonly Dictionary<EarthlyBranch, 天将> 字典;
        private readonly Dictionary<天将, EarthlyBranch> 逆字典;
        
        internal 天将盘(Guid 所用插件, 天地盘 天地盘, I去冗天将盘 天将盘)
        {
            this.所用插件 = 所用插件;
            this.天地盘 = 天地盘;

            this.字典 = new(12);
            this.逆字典 = new(12);
            for (int 序数 = 1; 序数 <= 12; 序数++)
            {
                EarthlyBranch 地支 = new EarthlyBranch(序数);
                天将 天将 = 天将盘.取乘将(地支);
                this.字典[地支] = 天将;
                this.逆字典[天将] = 地支;
            }
            Debug.Assert(this.字典.Count is 12);
            if (this.逆字典.Count is not 12)
                throw new 起课失败异常("天将插件起出的天将和地支不一一对应。");
        }

        public Guid 所用插件 { get; }

        public EarthlyBranch 取临地(天将 天将)
        {
            var 乘神 = this.取乘神(天将);
            return this.天地盘.取临地(乘神);
        }

        public 天将 取乘将(EarthlyBranch 地支)
        {
            Debug.Assert(this.字典.ContainsKey(地支));
            return this.字典[地支];
        }

        public EarthlyBranch 取乘神(天将 天将)
        {
            Debug.Assert(this.逆字典.ContainsKey(天将));
            return this.逆字典[天将];
        }
    }
}
