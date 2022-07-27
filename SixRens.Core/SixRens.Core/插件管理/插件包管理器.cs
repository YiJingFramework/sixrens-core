using SixRens.Api;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using static SixRens.Core.插件管理.经过解析的预设;
using static SixRens.Core.插件管理.预设;
using static SixRens.Core.插件管理.预设.实体题目和所属插件识别码;

namespace SixRens.Core.插件管理
{
    public class 插件包管理器
    {
        private readonly DirectoryInfo _文件夹;
        private readonly List<插件包> _插件包;
        public IReadOnlyList<插件包> 插件包 { get; }

        private readonly Dictionary<Guid, 插件和所属插件包<I地盘插件>> _地盘插件;
        public IReadOnlyCollection<插件和所属插件包<I地盘插件>> 地盘插件
            => _地盘插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I天盘插件>> _天盘插件;
        public IReadOnlyCollection<插件和所属插件包<I天盘插件>> 天盘插件
            => _天盘插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I四课插件>> _四课插件;
        public IReadOnlyCollection<插件和所属插件包<I四课插件>> 四课插件
            => _四课插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I三传插件>> _三传插件;
        public IReadOnlyCollection<插件和所属插件包<I三传插件>> 三传插件
            => _三传插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I天将插件>> _天将插件;
        public IReadOnlyCollection<插件和所属插件包<I天将插件>> 天将插件
            => _天将插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I年命插件>> _年命插件;
        public IReadOnlyCollection<插件和所属插件包<I年命插件>> 年命插件
            => _年命插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I神煞插件>> _神煞插件;
        public IReadOnlyCollection<插件和所属插件包<I神煞插件>> 神煞插件
            => _神煞插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I课体插件>> _课体插件;
        public IReadOnlyCollection<插件和所属插件包<I课体插件>> 课体插件
            => _课体插件.Values;

        private readonly Dictionary<Guid, 插件和所属插件包<I参考插件>> _参考插件;
        public IReadOnlyCollection<插件和所属插件包<I参考插件>> 参考插件
            => _参考插件.Values;

        private string 配置文件路径 => Path.GetFullPath("packages", this._文件夹.FullName);
        private void 更新配置文件()
        {
            using var writer = File.CreateText(this.配置文件路径);
            foreach (var 名称 in this._插件包)
                writer.WriteLine(名称.本地识别码);
        }
        private FileInfo 新建随机文件
        {
            get
            {
                for (; ; )
                {
                    var result = Path.GetFullPath(Path.GetRandomFileName(), this._文件夹.FullName);
                    FileInfo info = new FileInfo(result);
                    if (!info.Exists)
                        return info;
                }
            }
        }

        public 插件包管理器(DirectoryInfo 插件包储存文件夹)
        {
            this._文件夹 = 插件包储存文件夹;
            this._文件夹.Create();
            this._插件包 = new();
            this.插件包 = new ReadOnlyCollection<插件包>(this._插件包);

            this._地盘插件 = new();
            this._天盘插件 = new();
            this._四课插件 = new();
            this._三传插件 = new();
            this._天将插件 = new();
            this._年命插件 = new();
            this._神煞插件 = new();
            this._课体插件 = new();
            this._参考插件 = new();

            var info = new FileInfo(this.配置文件路径);
            if (info.Exists)
            {
                var lines = File.ReadAllLines(info.FullName);
                foreach (var fileName in lines)
                {
                    var path = Path.GetFullPath(fileName, this._文件夹.FullName);
                    using var s = new FileStream(path, FileMode.Open);
                    插件包 包 = new 插件包(s, fileName);

                    try
                    {
                        foreach (var 插件 in 包.地盘插件)
                        {
                            this._地盘插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.天盘插件)
                        {
                            this._天盘插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.四课插件)
                        {
                            this._四课插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.三传插件)
                        {
                            this._三传插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.天将插件)
                        {
                            this._天将插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.年命插件)
                        {
                            this._年命插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.神煞插件)
                        {
                            this._神煞插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.课体插件)
                        {
                            this._课体插件.Add(插件.插件识别码, new(插件, 包));
                        }
                        foreach (var 插件 in 包.参考插件)
                        {
                            this._参考插件.Add(插件.插件识别码, new(插件, 包));
                        }
                    }
                    catch
                    {
                        包.插件包上下文.Unload();
                        throw;
                    }

                    this._插件包.Add(包);
                }
            }
        }

        /// <returns>成功加载但有重复识别码时，将卸载之，并会返回 null 。</returns>
        public 插件包? 从外部加载插件包(Stream 插件包流)
        {
            var info = this.新建随机文件;
            插件包 包;
            using (var file = info.Open(FileMode.CreateNew))
            {
                插件包流.CopyTo(file);
                file.Flush();
                file.Position = 0;
                包 = new 插件包(插件包流, info.Name);
            }

            foreach (var 插件 in 包.地盘插件)
            {
                if(this._地盘插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.天盘插件)
            {
                if (this._天盘插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.四课插件)
            {
                if (this._四课插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.三传插件)
            {
                if (this._三传插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.天将插件)
            {
                if (this._天将插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.年命插件)
            {
                if (this._年命插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.神煞插件)
            {
                if (this._神煞插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.课体插件)
            {
                if (this._课体插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }
            foreach (var 插件 in 包.参考插件)
            {
                if (this._参考插件.ContainsKey(插件.插件识别码))
                {
                    包.插件包上下文.Unload();
                    return null;
                }
            }

            foreach (var 插件 in 包.地盘插件)
            {
                this._地盘插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.天盘插件)
            {
                this._天盘插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.四课插件)
            {
                this._四课插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.三传插件)
            {
                this._三传插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.天将插件)
            {
                this._天将插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.年命插件)
            {
                this._年命插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.神煞插件)
            {
                this._神煞插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.课体插件)
            {
                this._课体插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.参考插件)
            {
                this._参考插件.Add(插件.插件识别码, new(插件, 包));
            }

            this._插件包.Add(包);
            this.更新配置文件();
            return 包;
        }

        public void 移除插件包(插件包 包, bool 卸载上下文 = true)
        {
            if (this._插件包.Remove(包))
            {
                foreach (var 插件 in 包.地盘插件)
                {
                    _ = this._地盘插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.天盘插件)
                {
                    _ = this._天盘插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.四课插件)
                {
                    _ = this._四课插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.三传插件)
                {
                    _ = this._三传插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.天将插件)
                {
                    _ = this._天将插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.年命插件)
                {
                    _ = this._年命插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.神煞插件)
                {
                    _ = this._神煞插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.课体插件)
                {
                    _ = this._课体插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.参考插件)
                {
                    _ = this._参考插件.Remove(插件.插件识别码);
                }

                if (卸载上下文)
                    包.插件包上下文.Unload();
                this.更新配置文件();
            }
        }

        private static bool 找出插件<T插件>(
            Guid? 欲找的插件,
            Dictionary<Guid, 插件和所属插件包<T插件>> 插件表,
            [MaybeNullWhen(false)] out T插件 插件)
            where T插件 : I插件
        {
            if (!欲找的插件.HasValue)
            {
                插件 = default;
                return false;
            }

            if (!插件表.TryGetValue(欲找的插件.Value, out var 插件和所属插件包))
            {
                插件 = default;
                return false;
            }

            插件 = 插件和所属插件包.插件;
            return true;
        }

        private static bool 找出插件<T插件>(
            IEnumerable<Guid> 欲找的插件, Dictionary<Guid, 插件和所属插件包<T插件>> 插件表,
            out List<T插件> 插件)
            where T插件 : I插件
        {
            插件 = new();
            foreach (var c in 欲找的插件)
            {
                if (!插件表.TryGetValue(c, out var 插件和所属插件包))
                    return false;
                插件.Add(插件和所属插件包.插件);
            }
            return true;
        }

        public 经过解析的预设? 解析预设(预设 预设)
        {
            if (!找出插件(预设.地盘插件, this._地盘插件, out var 地盘插件) ||
                !找出插件(预设.天盘插件, this._天盘插件, out var 天盘插件) ||
                !找出插件(预设.四课插件, this._四课插件, out var 四课插件) ||
                !找出插件(预设.三传插件, this._三传插件, out var 三传插件) ||
                !找出插件(预设.天将插件, this._天将插件, out var 天将插件) ||
                !找出插件(预设.年命插件, this._年命插件, out var 年命插件) ||
                !找出插件(预设.神煞插件, this._神煞插件, out var 神煞插件) ||
                !找出插件(预设.课体插件, this._课体插件, out var 课体插件) ||
                !找出插件(预设.参考插件, this._参考插件, out var 参考插件))
                return null;

            List<实体题目表和所属插件<I神煞插件>> 神煞插件和启用的神煞 = new();
            HashSet<实体题目和所属插件识别码> 神煞启用 = new(预设.神煞启用, 哈希器.实例);
            HashSet<实体题目和所属插件识别码> 神煞禁用 = new(预设.神煞禁用, 哈希器.实例);

            bool 存在未显式指定的神煞 = false;
            bool 存在同时指定了启用和禁用的神煞 = false;
            foreach (var 插件 in 神煞插件)
            {
                List<string> 题目 = new();
                foreach (var 神煞名 in 插件.支持的神煞)
                {
                    var 神煞 = new 实体题目和所属插件识别码(神煞名.神煞名, 插件.插件识别码);
                    if (神煞禁用.Contains(神煞))
                    {
                        if (神煞启用.Remove(神煞))
                            存在同时指定了启用和禁用的神煞 = true;
                        continue;
                    }

                    if (!神煞启用.Remove(神煞))
                        存在未显式指定的神煞 = true;

                    题目.Add(神煞名.神煞名);
                    continue;
                }
                神煞插件和启用的神煞.Add(new(插件, (IEnumerable<string>)题目));
            }
            bool 存在指定启用但未找到的神煞 = 神煞启用.Count > 0;


            List<实体题目表和所属插件<I课体插件>> 课体插件和启用的课体 = new();
            HashSet<实体题目和所属插件识别码> 课体启用 = new(预设.课体启用, 哈希器.实例);
            HashSet<实体题目和所属插件识别码> 课体禁用 = new(预设.课体禁用, 哈希器.实例);

            bool 存在未显式指定的课体 = false;
            bool 存在同时指定了启用和禁用的课体 = false;
            foreach (var 插件 in 课体插件)
            {
                List<string> 题目 = new();
                foreach (var 课体名 in 插件.支持的课体)
                {
                    var 课体 = new 实体题目和所属插件识别码(课体名.课体名, 插件.插件识别码);
                    if (课体禁用.Contains(课体))
                    {
                        if (课体启用.Remove(课体))
                            存在同时指定了启用和禁用的课体 = true;
                        continue;
                    }

                    if (!课体启用.Remove(课体))
                        存在未显式指定的课体 = true;

                    题目.Add(课体名.课体名);
                    continue;
                }
                课体插件和启用的课体.Add(new(插件, (IEnumerable<string>)题目));
            }
            bool 存在指定启用但未找到的课体 = 课体启用.Count > 0;

            return new(
                地盘插件,
                天盘插件,
                四课插件,
                三传插件,
                天将插件,
                年命插件,
                神煞插件和启用的神煞,
                存在未显式指定的神煞,
                存在指定启用但未找到的神煞,
                存在同时指定了启用和禁用的神煞,
                课体插件和启用的课体,
                存在未显式指定的课体,
                存在指定启用但未找到的课体,
                存在同时指定了启用和禁用的课体,
                参考插件);
        }
    }
}
