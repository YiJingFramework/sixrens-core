using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 天盘插件一 : I天盘插件, I天盘
    {
        public string? 插件名 => "测试用天盘一（子加子）";

        public Guid 插件识别码 => new Guid("26399D30-8C4F-463A-8823-FE130072AC42");

        public EarthlyBranch 取天神(EarthlyBranch 地盘支)
        {
            return 地盘支;
        }

        public I天盘 获取天盘(I年月日时 年月日时, I地盘 地盘)
        {
            return this;
        }
    }
}
