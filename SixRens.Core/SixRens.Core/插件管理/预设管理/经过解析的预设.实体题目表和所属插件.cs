using SixRens.Api;

namespace SixRens.Core.插件管理.预设管理
{
    public sealed partial class 经过解析的预设
    {
        public sealed record 实体题目和所属插件<T插件>(
            T插件 插件,
            string 题目) : IEquatable<实体题目和所属插件<T插件>>
            where T插件 : I插件
        { }
    }
}
