using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 地盘插件一 : I地盘插件, I地盘
    {
        public string? 插件名 => "测试用地盘一（子）";

        public Guid 插件识别码 => new Guid("A044E7CA-541C-4801-A2EC-6BEA4EFE2E3C");

        public EarthlyBranch 取地支(EarthlyBranch 位置)
        {
            return 位置;
        }

        public I地盘 获取地盘(Guid 壬式识别码, I年月日时 年月日时)
        {
            return this;
        }
    }
}
