using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化占断参考 : I占断参考
    {
        public 可序列化占断参考(占断参考 参考)
        {
            this.插件 = 参考.所用插件;
            this.题目 = 参考.题目;
            this.内容 = 参考.内容;
        }

        public 可序列化占断参考() { }
        public Guid 插件 { get; init; }
        public EarthlyBranch? 相关宫位 { get; init; }
        public string? 题目 { get; init; }
        public string? 内容 { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        string I占断参考.题目 => 题目 ?? string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        string I占断参考.内容 => 内容 ?? string.Empty;

        public 占断参考 转占断参考()
        {
            return new 占断参考(this.插件, this);
        }
    }
}