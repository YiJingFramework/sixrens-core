using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 天将插件二 : I天将插件, I天将盘
    {
        public string? 插件名 => "测试用天将二（加一转）";

        public Guid 插件识别码 => new Guid("99659CDA-BBD1-4B4C-B935-3CA7C2590683");

        public 天将 取乘将(EarthlyBranch 天神)
        {
            return (天将)(天神.Index + 1);
        }

        public I天将盘 获取天将盘(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传)
        {
            return this;
        }
    }
}
