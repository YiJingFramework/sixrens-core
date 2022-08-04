using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Core.壬式生成;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化天将盘 : I去冗天将盘
    {
        public 可序列化天将盘(天将盘 天将盘)
        {
            this.插件 = 天将盘.所用插件;
            this.表 = Enumerable.Range(1, 12)
                .Select(i => 天将盘.取乘将(new EarthlyBranch(i)))
                .Select(e => (int)e)
                .ToArray();
        }
        public 可序列化天将盘() { }
        public Guid 插件 { get; init; }
        public int[]? 表 { get; init; }

        天将 I去冗天将盘.取乘将(EarthlyBranch 天神)
        {
            if (this.表 is null)
                return (天将)0;
            var index = 天神.Index - 1;
            if (index >= this.表.Length)
                return (天将)0;
            return (天将)this.表[index];
        }

        public 去冗天将盘 转天将盘()
        {
            return new 去冗天将盘(this.插件, this);
        }
    }
}