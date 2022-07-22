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
    public class 地盘插件二 : I地盘插件, I地盘
    {
        public string? 插件名 => "测试用地盘二（丑）";

        public Guid 插件识别码 => new Guid("9A661FCE-E7B4-4714-98DD-CFE2BC80D4EA");

        public EarthlyBranch 取地支(EarthlyBranch 位置)
        {
            return 位置.Next(-1);
        }

        public I地盘 获取地盘(I年月日时 年月日时)
        {
            return this;
        }
    }
}
