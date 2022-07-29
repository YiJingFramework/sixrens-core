using SixRens.Api;

namespace SixRens.Core.插件管理.插件包管理
{
    public sealed record 插件和所属插件包<T插件>(
        T插件 插件,
        插件包 插件包)
        where T插件 : I插件
    { }
}
