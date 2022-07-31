namespace SixRens.Core.插件管理.插件包管理
{
    public interface I插件包管理器储存器
    {
        IEnumerable<(string 插件包文件名, Stream 插件包)> 获取所有插件包文件();

        string 生成新的插件包文件名();

        void 储存插件包文件(string 插件包文件名, Stream 插件包);

        void 移除插件包文件(string 插件包文件名);
    }
}
