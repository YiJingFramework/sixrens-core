using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed record 本命信息(
        YinYang 性别,
        EarthlyBranch 本命)
    { }
}
