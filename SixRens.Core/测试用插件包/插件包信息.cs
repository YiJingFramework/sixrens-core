using SixRens.Api;

namespace 测试用插件包
{
    public sealed record 插件包信息(
        string 名称,
        string 版本号,
        string? 网址,
        string 主程序集,
        IEnumerable<Type> 插件类) : I插件包信息
    { }
}
