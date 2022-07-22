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
    public class 四课插件一 : I四课插件, I四课
    {
        public string? 插件名 => "测试用四课一（全一）";

        public Guid 插件识别码 => new Guid("1E033E83-D7B8-4244-B349-3A0CF283E690");

        public HeavenlyStem 日 => new(1);

        public EarthlyBranch 日阳 => new(1);

        public EarthlyBranch 日阴 => new(1);

        public EarthlyBranch 辰 => new(1);

        public EarthlyBranch 辰阳 => new(1);

        public EarthlyBranch 辰阴 => new(1);

        public I四课 获取四课(I年月日时 年月日时, I地盘 地盘, I天盘 天盘)
        {
            return this;
        }
    }
}
