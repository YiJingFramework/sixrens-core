﻿namespace SixRens.Core.插件管理.插件包管理
{
    public interface I插件包管理器储存器
    {
        ValueTask<string> 储存插件包文件(Stream 插件包);

        ValueTask<IEnumerable<(string 插件包本地识别码, Stream 插件包)>> 获取所有插件包文件();

        ValueTask<Stream?> 获取插件包文件(string 插件包本地识别码);

        ValueTask 移除插件包文件(string 插件包本地识别码);
    }
}
