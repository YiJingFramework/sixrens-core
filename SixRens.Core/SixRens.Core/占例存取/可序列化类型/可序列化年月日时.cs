using SixRens.Api.实体;
using SixRens.Api.实体.起课信息;
using SixRens.Core.年月日时;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化年月日时
    {
        public int 年干 { get; init; }
        public int 年支 { get; init; }
        public int 月干 { get; init; }
        public int 月支 { get; init; }
        public int 日干 { get; init; }
        public int 日支 { get; init; }
        public int 时干 { get; init; }
        public int 时支 { get; init; }
        public bool 昼占 { get; init; }
        public int 月将 { get; init; }

        public 可序列化年月日时(I年月日时 年月日时)
        {
            this.年干 = 年月日时.年干.Index;
            this.年支 = 年月日时.年支.Index;
            this.月干 = 年月日时.月干.Index;
            this.月支 = 年月日时.月支.Index;
            this.日干 = 年月日时.日干.Index;
            this.日支 = 年月日时.日支.Index;
            this.时干 = 年月日时.时干.Index;
            this.时支 = 年月日时.时支.Index;
            this.昼占 = 年月日时.昼占;
            this.月将 = 年月日时.月将.Index;
        }
        public 可序列化年月日时() { }

        public 自定义年月日时 转年月日时()
        {
            return new(
                new(this.年干),
                new(this.年支),
                new(this.月干),
                new(this.月支),
                new(this.日干),
                new(this.日支),
                new(this.时干),
                new(this.时支),
                this.昼占,
                new(this.月将));
        }
    }
}