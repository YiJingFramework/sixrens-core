using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体.壬式
{
    internal sealed record 四课(
        HeavenlyStem 日,
        EarthlyBranch 日阳,
        EarthlyBranch 日阴,
        EarthlyBranch 辰,
        EarthlyBranch 辰阳,
        EarthlyBranch 辰阴) : I四课
    { }
}
