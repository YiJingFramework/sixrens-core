using SixRens.Api;
using SixRens.Api.实体;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 年命 : I年命
    {
        internal 年命(Guid 所用插件, I年命 年命)
        {
            this.所用插件 = 所用插件;
            this.性别 = 年命.性别;
            this.本命 = 年命.本命;
            this.行年 = 年命.行年;
        }
        public Guid 所用插件 { get; }
        public YinYang 性别 { get; }
        public EarthlyBranch 本命 { get; }
        public EarthlyBranch 行年 { get; }
    }
}
