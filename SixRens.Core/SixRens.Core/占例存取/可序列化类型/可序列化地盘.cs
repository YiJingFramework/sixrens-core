using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化地盘 : I地盘
    {
        public 可序列化地盘(地盘 地盘)
        {
            this.插件 = 地盘.所用插件;
            this.表 = Enumerable.Range(1, 12)
                .Select(i => 地盘.取地支(new EarthlyBranch(i)))
                .Select(e => e.Index)
                .ToArray();
        }

        public 可序列化地盘() { }

        public Guid 插件 { get; init; }
        public int[]? 表 { get; init; }

        EarthlyBranch I地盘.取地支(EarthlyBranch 位置)
        {
            if (this.表 is null)
                return new(0);
            var index = 位置.Index - 1;
            if (index >= this.表.Length)
                return new(0);
            return new(this.表[index]);
        }

        public 地盘 转地盘()
        {
            return new 地盘(插件, this);
        }
    }
}