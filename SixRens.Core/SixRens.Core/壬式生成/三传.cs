using SixRens.Api;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 三传 : I三传
    {
        internal 三传(Guid 所用插件, I三传 三传)
        {
            this.所用插件 = 所用插件;
            this.初传 = 三传.初传;
            this.中传 = 三传.中传;
            this.末传 = 三传.末传;
        }
        public Guid 所用插件 { get; }
        public EarthlyBranch 初传 { get; }
        public EarthlyBranch 中传 { get; }
        public EarthlyBranch 末传 { get; }
    }
}
