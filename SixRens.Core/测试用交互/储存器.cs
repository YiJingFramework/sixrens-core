﻿using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;

namespace 测试用交互
{
    public sealed class 储存器 : I插件包管理器储存器, I预设管理器储存器
    {
        private readonly DirectoryInfo _文件夹;

        private FileInfo 从插件包名获取文件(string 插件包名)
        {
            var path = Path.GetFullPath($"srspg_{插件包名}", this._文件夹.FullName);
            return new FileInfo(path);
        }

        internal 储存器(string path)
        {
            this._文件夹 = new(path);
            this._文件夹.Create();
        }

        private string 生成新的插件包文件名()
        {
            for (; ; )
            {
                var rand = Path.GetRandomFileName();
                rand = Path.GetFileNameWithoutExtension(rand);
                var file = this.从插件包名获取文件(rand);
                if (!file.Exists)
                    return rand;
            }
        }

        public async ValueTask<string> 储存插件包文件(Stream 插件包)
        {
            var name = 生成新的插件包文件名();
            var file = this.从插件包名获取文件(name);
            using var f = file.Open(FileMode.Create);
            await 插件包.CopyToAsync(f);
            return name;
        }

        public ValueTask 移除插件包文件(string 插件包名)
        {
            var file = this.从插件包名获取文件(插件包名);
            file.Delete();
            return ValueTask.CompletedTask;
        }

        public ValueTask<IEnumerable<(string 插件包本地识别码, Stream 插件包)>> 获取所有插件包文件()
        {
            return ValueTask.FromResult(this._文件夹.EnumerateFiles()
                .Where(file => file.Name.StartsWith("srspg_"))
                .Select(file => (file.Name["srspg_".Length..],
                (Stream)file.Open(FileMode.Open, FileAccess.Read))));
        }

        public ValueTask<Stream?> 获取插件包文件(string 插件包文件名)
        {
            var file = 从插件包名获取文件(插件包文件名);
            return ValueTask.FromResult((Stream?)file.OpenRead());
        }

        private FileInfo 从预设名获取文件(string 预设名)
        {
            var path = Path.GetFullPath($"preset_{预设名}", this._文件夹.FullName);
            return new FileInfo(path);
        }

        public ValueTask<IEnumerable<(string 预设名, string 内容)>> 获取所有预设文件()
        {
            return ValueTask.FromResult(this._文件夹.EnumerateFiles()
                .Where(file => file.Name.StartsWith("preset_"))
                .Select(file => (file.Name["preset_".Length..],
                File.ReadAllText(file.FullName))));
        }

        public async ValueTask<bool> 新建预设文件(string 预设名)
        {
            var file = this.从预设名获取文件(预设名);
            if (file.Exists)
                return false;
            await File.WriteAllTextAsync(file.FullName, null);
            return true;
        }

        public async ValueTask 储存预设文件(string 预设名, string 内容)
        {
            var file = this.从预设名获取文件(预设名);
            await File.WriteAllTextAsync(file.FullName, 内容);
        }

        public ValueTask 移除预设文件(string 预设名)
        {
            var file = this.从预设名获取文件(预设名);
            file.Delete();
            return ValueTask.CompletedTask;
        }
    }
}
