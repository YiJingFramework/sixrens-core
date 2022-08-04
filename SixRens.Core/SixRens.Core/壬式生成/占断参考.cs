using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 占断参考 : I占断参考
    {
        internal 占断参考(Guid 所用插件, I占断参考 参考)
        {
            this.所用插件 = 所用插件;
            this.题目 = 参考.题目;
            this.内容 = 参考.内容;
            this.相关宫位 = 参考.相关宫位;
        }
        public Guid 所用插件 { get; }
        public string 题目 { get; }
        public EarthlyBranch? 相关宫位 { get; }
        public string 内容 { get; }
    }
}
