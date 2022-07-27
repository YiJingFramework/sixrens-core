using SixRens.Api.实体;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化课体 : I课体
    {
        public 可序列化课体(课体 课体)
        {
            this.插件 = 课体.所用插件;
            this.课名 = 课体.课体名;
        }
        public 可序列化课体() { }
        public Guid 插件 { get; init; }
        public string? 课名 { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string 课体名 => this.课名 ?? string.Empty;

        public 课体 转课体()
        {
            return new 课体(this.插件, this);
        }
    }
}