using SixRens.Api.实体;
using SixRens.Core.壬式生成;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化课体 : I课体内容
    {
        public 可序列化课体(课体 课体)
        {
            this.插件 = 课体.所用插件;
            this.课体名 = 课体.课体名;
        }
        public 可序列化课体() { }
        public Guid 插件 { get; init; }
        public string? 课体名 { get; init; }
        public bool 属此课体 { get; init; }

        public 课体 转课体()
        {
            return new 课体(this.插件, this.课体名 ?? string.Empty, this);
        }
    }
}