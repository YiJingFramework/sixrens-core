using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化四课 : I四课
    {
        public 可序列化四课(四课 四课)
        {
            this.插件 = 四课.所用插件;
            this.日 = 四课.日.Index;
            this.日阳 = 四课.日阳.Index;
            this.日阴 = 四课.日阴.Index;
            this.辰 = 四课.辰.Index;
            this.辰阳 = 四课.辰阳.Index;
            this.辰阴 = 四课.辰阴.Index;
        }
        public 可序列化四课() { }
        public Guid 插件 { get; init; }
        public int 日 { get; init; }
        public int 日阳 { get; init; }
        public int 日阴 { get; init; }
        public int 辰 { get; init; }
        public int 辰阳 { get; init; }
        public int 辰阴 { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        HeavenlyStem I四课.日 => new(this.日);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I四课.日阳 => new(this.日阳);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I四课.日阴 => new(this.日阴);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I四课.辰 => new(this.辰);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I四课.辰阳 => new(this.辰阳);

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        EarthlyBranch I四课.辰阴 => new(this.辰阴);

        public 四课 转四课()
        {
            return new 四课(this.插件, this);
        }
    }
}