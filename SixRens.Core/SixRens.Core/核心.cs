using SixRens.Core.实体.插件管理;

namespace SixRens.Core
{
    public sealed class 核心
    {
        public 插件包管理器 插件包管理器 { get; }
        public 核心(DirectoryInfo 插件包文件夹, DirectoryInfo 预设文件夹)
        {
            this.插件包管理器 = new 插件包管理器(插件包文件夹);
        }
    }
}
