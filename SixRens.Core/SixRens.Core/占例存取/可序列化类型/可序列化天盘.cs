using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化天盘 : I天盘
    {
        public 可序列化天盘(天盘 天盘)
        {
            this.插件 = 天盘.所用插件;
            this.表 = Enumerable.Range(1, 12)
                .Select(i => 天盘.取天神(new EarthlyBranch(i)))
                .Select(e => e.Index)
                .ToArray();
        }
        public 可序列化天盘() { }
        public Guid 插件 { get; init; }
        public int[]? 表 { get; init; }

        EarthlyBranch I天盘.取天神(EarthlyBranch 地盘支)
        {
            if (this.表 is null)
                return new(0);
            var index = 地盘支.Index - 1;
            if (index >= this.表.Length)
                return new(0);
            return new(this.表[index]);
        }

        public 天盘 转天盘()
        {
            return new 天盘(插件, this);
        }
    }
}