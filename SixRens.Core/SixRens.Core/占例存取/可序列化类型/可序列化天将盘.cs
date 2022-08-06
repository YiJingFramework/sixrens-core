using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化天将盘 : I去冗天将盘
    {
        public 可序列化天将盘(天将盘 天将盘)
        {
            this.插件 = 天将盘.所用插件;
            this.为顺行 = 天将盘.为顺行;
            this.贵人天盘所乘 = 天将盘.贵人天盘所乘.Index;
        }
        public 可序列化天将盘() { }
        public Guid 插件 { get; init; }

        public bool 为顺行 { get; set; }
        public int 贵人天盘所乘 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I去冗天将盘.贵人天盘所乘 => new(贵人天盘所乘);

        public 天将盘 转天将盘()
        {
            return new 天将盘(this.插件, this);
        }
    }
}