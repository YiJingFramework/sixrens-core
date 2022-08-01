using SixRens.Core.插件管理.插件包管理;
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

        public void 储存插件包文件(string 插件包名, Stream 插件包)
        {
            var file = this.从插件包名获取文件(插件包名);
            using var f = file.Open(FileMode.Create);
            插件包.CopyTo(f);
        }

        public string 生成新的插件包文件名()
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

        public void 移除插件包文件(string 插件包名)
        {
            var file = this.从插件包名获取文件(插件包名);
            file.Delete();
        }

        public IEnumerable<(string 插件包文件名, Stream 插件包)> 获取所有插件包文件()
        {
            return this._文件夹.EnumerateFiles()
                .Where(file => file.Name.StartsWith("srspg_"))
                .Select(file => (file.Name["srspg_".Length..],
                (Stream)file.Open(FileMode.Open, FileAccess.Read)));
        }

        public Stream 获取插件包文件(string 插件包文件名)
        {
            var file = 从插件包名获取文件(插件包文件名);
            return file.OpenRead();
        }

        private FileInfo 从预设名获取文件(string 预设名)
        {
            var path = Path.GetFullPath($"preset_{预设名}", this._文件夹.FullName);
            return new FileInfo(path);
        }

        public IEnumerable<(string 预设名, string 内容)> 获取所有预设文件()
        {
            return this._文件夹.EnumerateFiles()
                .Where(file => file.Name.StartsWith("preset_"))
                .Select(file => (file.Name["preset_".Length..],
                File.ReadAllText(file.FullName)));
        }

        public bool 新建预设文件(string 预设名)
        {
            var file = this.从预设名获取文件(预设名);
            if (file.Exists)
                return false;
            File.WriteAllText(file.FullName, null);
            return true;
        }

        public void 储存预设文件(string 预设名, string 内容)
        {
            var file = this.从预设名获取文件(预设名);
            File.WriteAllText(file.FullName, 内容);
        }

        public void 移除预设文件(string 预设名)
        {
            var file = this.从预设名获取文件(预设名);
            file.Delete();
        }
    }
}
