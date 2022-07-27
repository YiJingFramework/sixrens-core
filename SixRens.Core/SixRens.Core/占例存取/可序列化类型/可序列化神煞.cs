using SixRens.Api.实体;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化神煞 : I神煞内容
    {
        public 可序列化神煞(神煞 神煞)
        {
            this.插件 = 神煞.所用插件;
            this.神煞名 = 神煞.神煞名;
            this.所在神 = 神煞.所在神.Select(e => e.Index).ToArray();
        }
        public 可序列化神煞() { }
        public Guid 插件 { get; init; }
        public string? 神煞名 { get; init; }
        public int[]? 所在神 { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        IReadOnlyList<EarthlyBranch> I神煞内容.所在神
            => (this.所在神 ?? Array.Empty<int>()).Select(i => new EarthlyBranch(i)).ToArray();

        public 神煞 转神煞()
        {
            return new 神煞(this.插件, this.神煞名 ?? string.Empty, this);
        }
    }
}