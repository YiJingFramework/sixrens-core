using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化三传 : I三传
    {
        public 可序列化三传(三传 三传)
        {
            this.插件 = 三传.所用插件;
            this.初传 = 三传.初传.Index;
            this.中传 = 三传.中传.Index;
            this.末传 = 三传.末传.Index;
        }
        public 可序列化三传() { }
        public Guid 插件 { get; init; }

        public int 初传 { get; init; }

        public int 中传 { get; init; }

        public int 末传 { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I三传.初传 => new(this.初传);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I三传.中传 => new(this.中传);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I三传.末传 => new(this.末传);

        public 三传 转三传()
        {
            return new 三传(this.插件, this);
        }
    }
}