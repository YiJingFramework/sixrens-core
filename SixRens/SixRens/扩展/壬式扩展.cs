using SixRens.Api.实体;
using YiJingFramework.StemsAndBranches;

namespace SixRens.扩展
{
    public static class 壬式扩展
    {
        public static 天将 取所乘将(this 壬式 式, EarthlyBranch 上者)
        {
            return 式.天将盘.取乘将(上者);
        }
        public static 天将 取临我将(this 壬式 式, EarthlyBranch 下者)
        {
            return 式.天将盘.取乘将(式.取所乘神(下者));
        }
        public static EarthlyBranch 取所乘神(this 壬式 式, EarthlyBranch 下者)
        {
            return 式.天盘.获取同位支(式.地盘, 下者);
        }
        public static EarthlyBranch 取所临神(this 壬式 式, EarthlyBranch 上者)
        {
            return 式.地盘.获取同位支(式.天盘, 上者);
        }
    }
}
