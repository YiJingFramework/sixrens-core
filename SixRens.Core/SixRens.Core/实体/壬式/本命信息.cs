using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体.壬式
{
    public sealed record 本命信息(
        YinYang 性别,
        EarthlyBranch 本命)
    { }
}
