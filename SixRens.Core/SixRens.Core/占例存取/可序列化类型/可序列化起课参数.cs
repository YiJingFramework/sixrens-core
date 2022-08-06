using SixRens.Core.壬式生成;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化起课参数
    {
        public 可序列化起课参数(起课参数 起课信息)
        {
            this.年月日时 = new(起课信息.年月日时);
            this.课主年命 = 起课信息.课主年命 is null ? null : new(起课信息.课主年命);
            this.对象年命 = 起课信息.对象年命.Select(年命 => new 可序列化年命(年命)).ToArray();
        }

        public 可序列化起课参数() { }

        public 可序列化年月日时? 年月日时 { get; init; }
        public 可序列化年命? 课主年命 { get; init; }
        public 可序列化年命[]? 对象年命 { get; init; }

        public 起课参数 转起课信息()
        {
            return new 起课参数(
                (this.年月日时 ?? new()).转年月日时(),
                this.课主年命?.转年命(),
                (this.对象年命 ?? Array.Empty<可序列化年命>()).Select(年命 => 年命.转年命()));
        }
    }
}