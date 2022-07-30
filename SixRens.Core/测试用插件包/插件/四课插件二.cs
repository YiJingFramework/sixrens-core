using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 四课插件二 : I四课插件, I四课
    {
        public string? 插件名 => "测试用四课二（全二）";

        public Guid 插件识别码 => new Guid("E1066754-0C8D-4820-9EB1-12614CB98C97");

        public HeavenlyStem 日 => new(2);

        public EarthlyBranch 日阳 => new(2);

        public EarthlyBranch 日阴 => new(2);

        public EarthlyBranch 辰 => new(2);

        public EarthlyBranch 辰阳 => new(2);

        public EarthlyBranch 辰阴 => new(2);

        public I四课 获取四课(Guid 壬式识别码, I年月日时 年月日时, I地盘 地盘, I天盘 天盘)
        {
            return this;
        }
    }
}
