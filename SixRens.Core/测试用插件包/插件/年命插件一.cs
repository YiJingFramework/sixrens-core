using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace 测试用插件包.插件
{
    public class 年命插件一 : I年命插件, I年命
    {
        public string? 插件名 => "测试用年命一（全一）";

        public Guid 插件识别码 => new Guid("B4273FC8-3256-4E1A-A59D-D1A0092A30C9");

        public YinYang 性别 => (YinYang)1;

        public EarthlyBranch 本命 => new(1);

        public EarthlyBranch 行年 => new(1);

        public I年命 获取年命(I年月日时 年月日时, I地盘 地盘, I天盘 天盘, I四课 四课, I三传 三传, I天将盘 天将盘, YinYang 性别, EarthlyBranch 本命)
        {
            return this;
        }
    }
}
