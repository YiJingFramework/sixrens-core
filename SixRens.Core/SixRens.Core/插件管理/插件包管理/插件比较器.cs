using SixRens.Api;
using System.Diagnostics.CodeAnalysis;

namespace SixRens.Core.插件管理.插件包管理
{
    public sealed class 插件比较器<T插件> : IComparer<T插件>, IEqualityComparer<T插件> where T插件 : I插件
    {
        public int Compare(T插件? x, T插件? y)
        {
            if (x is null)
                return y is null ? 0 : -1;
            if (y is null)
                return 1;
            return x.插件识别码.CompareTo(y.插件识别码);
        }

        public bool Equals(T插件? x, T插件? y)
        {
            if (x is null)
                return y is null;
            if (y is null)
                return false;
            return x.插件识别码.Equals(y.插件识别码);
        }

        public int GetHashCode([DisallowNull] T插件 obj)
        {
            return obj.插件识别码.GetHashCode();
        }
    }
}
