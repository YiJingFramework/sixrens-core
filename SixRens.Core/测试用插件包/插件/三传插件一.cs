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
    public class 三传插件一 : I三传插件, I三传
    {
        public string? 插件名 => "测试用三传一（全一）";

        public Guid 插件识别码 => new Guid("A7CBCCF9-A03D-4D65-8E1F-A5BC7631E14A");

        public EarthlyBranch 初传 => new(1);

        public EarthlyBranch 中传 => new(1);

        public EarthlyBranch 末传 => new(1);

        public I三传 获取三传(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课)
        {
            return this;
        }
    }
}
