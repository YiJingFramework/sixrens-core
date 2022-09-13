namespace SixRens.Core.插件管理.预设管理
{
    public interface I预设管理器储存器
    {
        ValueTask<IEnumerable<(string 预设名, string 内容)>> 获取所有预设文件();

        ValueTask<bool> 新建预设文件(string 预设名);

        ValueTask 储存预设文件(string 预设名, string 内容);

        ValueTask 移除预设文件(string 预设名);
    }
}
