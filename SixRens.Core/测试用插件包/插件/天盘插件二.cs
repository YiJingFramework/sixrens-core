using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 天盘插件二 : I天盘插件, I天盘
    {
        public string? 插件名 => "测试用天盘二（丑加子）";

        public Guid 插件识别码 => new Guid("33904586-37D2-4B2E-8C13-912531D25CE7");

        public EarthlyBranch 取天神(EarthlyBranch 地盘支)
        {
            return 地盘支.Next();
        }

        public I天盘 获取天盘(I年月日时 年月日时, I地盘 地盘)
        {
            return this;
        }
    }
}
