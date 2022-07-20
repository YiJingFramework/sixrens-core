using SixRens.Api.实体.壬式;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体
{
    internal sealed class 天盘 : I天盘
    {
        private readonly Dictionary<EarthlyBranch, EarthlyBranch> 字典;
        public 天盘(I天盘 天盘)
        {
            字典 = new(12);
            for (int 序数 = 1; 序数 <= 12; 序数++)
            {
                EarthlyBranch 地支 = new EarthlyBranch(序数);
                字典.Add(地支, 天盘.取天神(地支));
            }
        }

        public EarthlyBranch 取天神(EarthlyBranch 地盘支)
        {
            Debug.Assert(字典.ContainsKey(地盘支));
            return 字典[地盘支];
        }
    }
}
