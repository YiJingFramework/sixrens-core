using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.实体
{
    public sealed record 本命信息(
        YinYang 性别,
        EarthlyBranch 本命)
    { }
}
