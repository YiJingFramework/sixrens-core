using SixRens.Api.实体;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体
{
    internal sealed record 年命信息(
        YinYang 性别,
        EarthlyBranch 本命,
        EarthlyBranch 行年) : I年命
    { }
}
