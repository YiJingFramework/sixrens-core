using SixRens.Api.实体;
using SixRens.Api.实体.起课信息;
using SixRens.Core.年月日时;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed record 年命(
        YinYang 性别, 
        EarthlyBranch 本命,
        EarthlyBranch 行年) : I年命
    {
        private static EarthlyBranch 判断行年(YinYang 性别, EarthlyBranch 本命, EarthlyBranch 当前年)
        {
            if (性别.IsYang)
            {
                const int 寅数 = 3;
                return new(寅数 + 当前年.Index - 本命.Index);
            }
            else
            {
                const int 申数 = 9;
                return new(申数 - 当前年.Index + 本命.Index);
            }
        }

        public 年命(I年命 年命)
            : this(年命.性别, 年命.本命, 年命.行年)
        { }

        public 年命(YinYang 性别, EarthlyBranch 本命, I年月日时 年月日时)
            : this(性别, 本命, 判断行年(性别, 本命, 年月日时.年支))
        { }

        public 年命(YinYang 性别, EarthlyBranch 本命, I年月日时信息 年月日时)
            : this(性别, 本命, 判断行年(性别, 本命, 年月日时.年支)) 
        { }
    }
}
