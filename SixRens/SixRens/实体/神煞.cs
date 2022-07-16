using SixRens.Api.实体;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.StemsAndBranches;

namespace SixRens.实体
{
    internal sealed record 神煞(
        string 神煞名,
        IReadOnlyList<EarthlyBranch> 所在神) : I神煞
    { }
}
