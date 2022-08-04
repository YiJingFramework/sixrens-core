using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 天将插件一 : I天将插件, I去冗天将盘
    {
        public string? 插件名 => "测试用天将一（直转）";

        public Guid 插件识别码 => new Guid("5E74DE74-1C7E-4DC9-A3BD-CD2F25DD0811");

        public 天将 取乘将(EarthlyBranch 天神)
        {
            return (天将)天神.Index;
        }

        public I去冗天将盘 获取天将盘(I起课信息 起课信息, I天地盘 天地盘, I四课 四课, I三传 三传)
        {
            return this;
        }
    }
}
