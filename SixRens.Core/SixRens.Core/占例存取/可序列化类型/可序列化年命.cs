using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化年命 : I年命
    {
        public 可序列化年命(年命 年命)
        {
            this.插件 = 年命.所用插件;
            this.性别 = 年命.性别.IsYang;
            this.本命 = 年命.本命.Index;
            this.行年 = 年命.行年.Index;
        }
        public 可序列化年命() { }
        public Guid 插件 { get; init; }
        public bool 性别 { get; init; }
        public int 本命 { get; init; }
        public int 行年 { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        YinYang I年命.性别 => new(性别);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I年命.本命 => new(本命);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I年命.行年 => new(行年);

        public 年命 转年命()
        {
            return new 年命(插件, this);
        }
    }
}