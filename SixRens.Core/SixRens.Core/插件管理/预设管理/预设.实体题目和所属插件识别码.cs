using System.Diagnostics.CodeAnalysis;

namespace SixRens.Core.插件管理.预设管理
{
    public sealed partial class 预设
    {
        public sealed record 实体题目和所属插件识别码(
            string 题目,
            Guid 插件) : IEquatable<实体题目和所属插件识别码>
        {
            public sealed class 哈希器 : IEqualityComparer<实体题目和所属插件识别码>
            {
                public static 哈希器 实例 { get; } = new();

                public bool Equals(实体题目和所属插件识别码? x, 实体题目和所属插件识别码? y)
                {
                    if (x is null)
                        return y is null;
                    return x.Equals(y);
                }

                public int GetHashCode([DisallowNull] 实体题目和所属插件识别码 obj)
                {
                    return obj.GetHashCode();
                }
            }
        }
    }
}
