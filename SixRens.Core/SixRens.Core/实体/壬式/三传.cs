using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体.壬式
{
    internal sealed record 三传(
        EarthlyBranch 初传,
        EarthlyBranch 中传,
        EarthlyBranch 末传) : I三传
    { }
}
