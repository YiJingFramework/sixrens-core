using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 四课 : I四课
    {
        internal 四课(Guid 所用插件, I四课 四课)
        {
            this.所用插件 = 所用插件;
            this.日 = 四课.日;
            this.日阳 = 四课.日阳;
            this.日阴 = 四课.日阴;
            this.辰 = 四课.辰;
            this.辰阳 = 四课.辰阳;
            this.辰阴 = 四课.辰阴;
        }
        public Guid 所用插件 { get; }

        public HeavenlyStem 日 { get; }
        public EarthlyBranch 日阳 { get; }
        public EarthlyBranch 日阴 { get; }
        public EarthlyBranch 辰 { get; }
        public EarthlyBranch 辰阳 { get; }
        public EarthlyBranch 辰阴 { get; }
    }
}
