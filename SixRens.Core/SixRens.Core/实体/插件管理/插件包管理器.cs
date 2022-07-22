using SixRens.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixRens.Core.实体.插件管理
{
    public class 插件包管理器
    {
        private readonly DirectoryInfo _文件夹;
        private readonly List<插件包> _插件包;

        public IReadOnlyCollection<插件包> 插件包 { get; }
        public IEnumerable<插件和所属插件包<I地盘插件>> 地盘插件
            => _插件包.SelectMany(
                包 => 包.地盘插件, 
                (包, 插件) => new 插件和所属插件包<I地盘插件>(插件, 包)
                );
        public IEnumerable<插件和所属插件包<I天盘插件>> 天盘插件
            => _插件包.SelectMany(
                包 => 包.天盘插件, 
                (包, 插件) => new 插件和所属插件包<I天盘插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I四课插件>> 四课插件
            => _插件包.SelectMany(
                包 => 包.四课插件,
                (包, 插件) => new 插件和所属插件包<I四课插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I三传插件>> 三传插件
            => _插件包.SelectMany(
                包 => 包.三传插件,
                (包, 插件) => new 插件和所属插件包<I三传插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I天将插件>> 天将插件
            => _插件包.SelectMany(
                包 => 包.天将插件,
                (包, 插件) => new 插件和所属插件包<I天将插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I年命插件>> 年命插件
            => _插件包.SelectMany(
                包 => 包.年命插件,
                (包, 插件) => new 插件和所属插件包<I年命插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I神煞插件>> 神煞插件
            => _插件包.SelectMany(
                包 => 包.神煞插件,
                (包, 插件) => new 插件和所属插件包<I神煞插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I课体插件>> 课体插件
            => _插件包.SelectMany(
                包 => 包.课体插件,
                (包, 插件) => new 插件和所属插件包<I课体插件>(插件, 包));
        public IEnumerable<插件和所属插件包<I参考插件>> 参考插件
            => _插件包.SelectMany(
                包 => 包.参考插件,
                (包, 插件) => new 插件和所属插件包<I参考插件>(插件, 包));

        private string 配置文件路径 => Path.GetFullPath("packages", _文件夹.FullName);
        private void 更新配置文件()
        {
            using var writer = File.CreateText(配置文件路径);
            foreach (var 名称 in _插件包)
                writer.WriteLine(名称.本地识别码);
        }
        private FileInfo 新建随机文件
        {
            get
            {
                for (; ; )
                {
                    var result = Path.GetFullPath(Path.GetRandomFileName(), _文件夹.FullName);
                    FileInfo info = new FileInfo(result);
                    if (!info.Exists)
                        return info;
                }
            }
        }

        internal 插件包管理器(DirectoryInfo 插件包储存文件夹)
        {
            this._文件夹 = 插件包储存文件夹;
            this._文件夹.Create();
            this._插件包 = new();
            this.插件包 = new ReadOnlyCollection<插件包>(this._插件包);

            var info = new FileInfo(配置文件路径);
            if (info.Exists)
            {
                var lines = File.ReadAllLines(info.FullName);
                foreach (var path in
                    lines.Select(line => Path.GetFullPath(line, this._文件夹.FullName)))
                {
                    using var s = new FileStream(path, FileMode.Open);
                    插件包 包 = new 插件包(s, info.Name);
                    this._插件包.Add(包);
                }
            }
        }

        public 插件包 从外部加载插件包(Stream 插件包流)
        {
            var info = 新建随机文件;
            插件包 包;
            using (var file = info.Open(FileMode.CreateNew))
            {
                插件包流.CopyTo(file);
                file.Flush();
                file.Position = 0;
                包 = new 插件包(插件包流, info.Name);
            }
            this._插件包.Add(包);
            更新配置文件();
            return 包;
        }

        public void 移除插件包(插件包 包)
        {
            if (this._插件包.Remove(包))
            {
                // 包.插件包上下文.Unload();
                更新配置文件();
            }
        }
    }
}
