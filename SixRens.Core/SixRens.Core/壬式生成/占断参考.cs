using SixRens.Api.实体;

namespace SixRens.Core.壬式生成
{
    public sealed class 占断参考 : I占断参考
    {
        internal 占断参考(Guid 所用插件, string 参考题目, I占断参考内容 参考内容)
        {
            this.所用插件 = 所用插件;
            this.题目 = 参考题目;
            this.内容 = 参考内容.内容;
        }
        public Guid 所用插件 { get; }
        public string 题目 { get; }
        public string? 内容 { get; }
    }
}
