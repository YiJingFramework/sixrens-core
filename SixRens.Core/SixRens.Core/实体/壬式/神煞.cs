using SixRens.Api.实体;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.实体.壬式
{
    internal sealed record 神煞(
        string 神煞名,
        IReadOnlyList<EarthlyBranch> 所在神) : I神煞
    { }
}
