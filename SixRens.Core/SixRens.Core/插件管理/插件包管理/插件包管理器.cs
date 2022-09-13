using Nito.AsyncEx;
using SixRens.Api;
using SixRens.Core.插件管理.预设管理;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static SixRens.Core.插件管理.预设管理.经过解析的预设;
using static SixRens.Core.插件管理.预设管理.预设;
using static SixRens.Core.插件管理.预设管理.预设.实体题目和所属插件识别码;

namespace SixRens.Core.插件管理.插件包管理
{
    public sealed class 插件包管理器 : IDisposable
    {
        private readonly I插件包管理器储存器 _储存器;
        private readonly List<插件包> _插件包;
        public IReadOnlyList<插件包> 插件包 { get; }

        private readonly Dictionary<Guid, 插件和所属插件包<I三传插件>> _三传插件;
        public IReadOnlyDictionary<Guid, 插件和所属插件包<I三传插件>> 三传插件 { get; }

        private readonly Dictionary<Guid, 插件和所属插件包<I天将插件>> _天将插件;
        public IReadOnlyDictionary<Guid, 插件和所属插件包<I天将插件>> 天将插件 { get; }

        private readonly Dictionary<Guid, 插件和所属插件包<I神煞插件>> _神煞插件;
        public IReadOnlyDictionary<Guid, 插件和所属插件包<I神煞插件>> 神煞插件 { get; }

        private readonly Dictionary<Guid, 插件和所属插件包<I课体插件>> _课体插件;
        public IReadOnlyDictionary<Guid, 插件和所属插件包<I课体插件>> 课体插件 { get; }

        private readonly Dictionary<Guid, 插件和所属插件包<I参考插件>> _参考插件;
        public IReadOnlyDictionary<Guid, 插件和所属插件包<I参考插件>> 参考插件 { get; }

        public static async ValueTask<插件包管理器> 创建插件包管理器(I插件包管理器储存器 插件包管理器储存器)
        {
            var result = new 插件包管理器(插件包管理器储存器);
            await result.初始化();
            return result;
        }

        private 插件包管理器(I插件包管理器储存器 插件包管理器储存器)
        {
            this._储存器 = 插件包管理器储存器;

            this._信号量锁 = new(1);
            this._插件包 = new();
            this.插件包 = new ReadOnlyCollection<插件包>(this._插件包);

            this._三传插件 = new();
            this.三传插件 = new ReadOnlyDictionary<Guid, 插件和所属插件包<I三传插件>>(this._三传插件);
            this._天将插件 = new();
            this.天将插件 = new ReadOnlyDictionary<Guid, 插件和所属插件包<I天将插件>>(this._天将插件);
            this._神煞插件 = new();
            this.神煞插件 = new ReadOnlyDictionary<Guid, 插件和所属插件包<I神煞插件>>(this._神煞插件);
            this._课体插件 = new();
            this.课体插件 = new ReadOnlyDictionary<Guid, 插件和所属插件包<I课体插件>>(this._课体插件);
            this._参考插件 = new();
            this.参考插件 = new ReadOnlyDictionary<Guid, 插件和所属插件包<I参考插件>>(this._参考插件);
        }

        private readonly SemaphoreSlim _信号量锁;

        private async ValueTask 初始化()
        {
            using var 锁 = await _信号量锁.LockAsync();
            foreach (var (插件包识别码, 插件包文件) in await this._储存器.获取所有插件包文件())
            {
                插件包 包 = new 插件包(插件包文件) {
                    本地识别码 = 插件包识别码
                };
                foreach (var 插件 in 包.三传插件)
                {
                    this._三传插件.Add(插件.插件识别码, new(插件, 包));
                }
                foreach (var 插件 in 包.天将插件)
                {
                    this._天将插件.Add(插件.插件识别码, new(插件, 包));
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
            }
            锁.Dispose(); // 我不确定 using 语句是否可能导致锁被提前释放。
        }

        public async ValueTask<(插件包 包, bool 出现重复而未加入包用完当释放)> 从外部加载插件包(Stream 插件包流)
        {
            var 插件包流复制 = new MemoryStream();
            插件包流.CopyTo(插件包流复制);

            插件包 包 = new 插件包(插件包流复制);

            using var 锁 = await _信号量锁.LockAsync();
            foreach (var 插件 in 包.三传插件)
            {
                if (this._三传插件.ContainsKey(插件.插件识别码))
                    return (包, true);
            }
            foreach (var 插件 in 包.天将插件)
            {
                if (this._天将插件.ContainsKey(插件.插件识别码))
                    return (包, true);
            }
            foreach (var 插件 in 包.神煞插件)
            {
                if (this._神煞插件.ContainsKey(插件.插件识别码))
                    return (包, true);
            }
            foreach (var 插件 in 包.课体插件)
            {
                if (this._课体插件.ContainsKey(插件.插件识别码))
                    return (包, true);
            }
            foreach (var 插件 in 包.参考插件)
            {
                if (this._参考插件.ContainsKey(插件.插件识别码))
                    return (包, true);
            }

            插件包流复制.Position = 0;
            var 包本地识别码 = await this._储存器.储存插件包文件(插件包流复制);
            包.本地识别码 = 包本地识别码;

            foreach (var 插件 in 包.三传插件)
            {
                this._三传插件.Add(插件.插件识别码, new(插件, 包));
            }
            foreach (var 插件 in 包.天将插件)
            {
                this._天将插件.Add(插件.插件识别码, new(插件, 包));
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
            锁.Dispose(); // 我不确定 using 语句是否可能导致锁被提前释放。
            return (包, false);
        }

        public async ValueTask<Stream?> 导出插件包文件(插件包 包)
        {
            if (包.本地识别码 is null)
                return null;
            return await this._储存器.获取插件包文件(包.本地识别码);
        }

        public async ValueTask 移除插件包(插件包 包)
        {
            using var 锁 = await _信号量锁.LockAsync();
            if (this._插件包.Remove(包))
            {
                Debug.Assert(包.本地识别码 is not null);
                await this._储存器.移除插件包文件(包.本地识别码);
                foreach (var 插件 in 包.三传插件)
                {
                    _ = this._三传插件.Remove(插件.插件识别码);
                }
                foreach (var 插件 in 包.天将插件)
                {
                    _ = this._天将插件.Remove(插件.插件识别码);
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
            }
            锁.Dispose(); // 我不确定 using 语句是否可能导致锁被提前释放。
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
            using var 锁 = _信号量锁.Lock();
            if (!找出插件(预设.三传插件, this._三传插件, out var 三传插件) ||
                !找出插件(预设.天将插件, this._天将插件, out var 天将插件) ||
                !找出插件(预设.神煞插件, this._神煞插件, out var 神煞插件) ||
                !找出插件(预设.课体插件, this._课体插件, out var 课体插件) ||
                !找出插件(预设.参考插件, this._参考插件, out var 参考插件))
                return null;

            Dictionary<int, 实体题目和所属插件<I神煞插件>> 显式指定启用的神煞
                = new(预设.神煞启用.Count);
            List<实体题目和所属插件<I神煞插件>> 未显式指定的神煞 = new();

            Dictionary<实体题目和所属插件识别码, int> 神煞启用
                = 预设.神煞启用.Select((神煞, 序号) => (序号, 神煞))
                .ToDictionary(序号和神煞 => 序号和神煞.神煞, 序号和神煞 => 序号和神煞.序号);
            HashSet<实体题目和所属插件识别码> 神煞禁用 = new(预设.神煞禁用, 哈希器.实例);

            bool 存在同时指定了启用和禁用的神煞 = false;
            foreach (var 插件 in 神煞插件)
            {
                foreach (var 神煞名 in 插件.支持神煞的名称)
                {
                    var 神煞 = new 实体题目和所属插件识别码(神煞名, 插件.插件识别码);
                    if (神煞禁用.Contains(神煞))
                    {
                        if (神煞启用.Remove(神煞))
                            存在同时指定了启用和禁用的神煞 = true;
                        continue;
                    }

                    if (神煞启用.Remove(神煞, out int 序号))
                    {
                        显式指定启用的神煞[序号] = new(插件, 神煞.题目);
                        continue;
                    }

                    未显式指定的神煞.Add(new(插件, 神煞.题目));
                    continue;
                }
            }
            bool 存在未显式指定的神煞 = 未显式指定的神煞.Count > 0;
            bool 存在指定启用但未找到的神煞 = 神煞启用.Count > 0;


            Dictionary<int, 实体题目和所属插件<I课体插件>> 显式指定启用的课体
                = new(预设.课体启用.Count);
            List<实体题目和所属插件<I课体插件>> 未显式指定的课体 = new();

            Dictionary<实体题目和所属插件识别码, int> 课体启用
                = 预设.课体启用.Select((课体, 序号) => (序号, 课体))
                .ToDictionary(序号和课体 => 序号和课体.课体, 序号和课体 => 序号和课体.序号);
            HashSet<实体题目和所属插件识别码> 课体禁用 = new(预设.课体禁用, 哈希器.实例);

            bool 存在同时指定了启用和禁用的课体 = false;
            foreach (var 插件 in 课体插件)
            {
                foreach (var 课体名 in 插件.支持的课体)
                {
                    var 课体 = new 实体题目和所属插件识别码(课体名, 插件.插件识别码);
                    if (课体禁用.Contains(课体))
                    {
                        if (课体启用.Remove(课体))
                            存在同时指定了启用和禁用的课体 = true;
                        continue;
                    }

                    if (课体启用.Remove(课体, out int 序号))
                    {
                        显式指定启用的课体[序号] = new(插件, 课体.题目);
                        continue;
                    }

                    未显式指定的课体.Add(new(插件, 课体.题目));
                    continue;
                }
            }
            bool 存在未显式指定的课体 = 未显式指定的课体.Count > 0;
            bool 存在指定启用但未找到的课体 = 神煞启用.Count > 0;

            var result = new 经过解析的预设(
                三传插件,
                天将插件,
                显式指定启用的神煞.Select(神煞 => 神煞.Value).Concat(未显式指定的神煞),
                存在未显式指定的神煞,
                存在指定启用但未找到的神煞,
                存在同时指定了启用和禁用的神煞,
                显式指定启用的课体.Select(课体 => 课体.Value).Concat(未显式指定的课体),
                存在未显式指定的课体,
                存在指定启用但未找到的课体,
                存在同时指定了启用和禁用的课体,
                参考插件);
            锁.Dispose(); // 我不确定 using 语句是否可能导致锁被提前释放。
            return result;
        }

        public void Dispose()
        {
            foreach (var 包 in _插件包)
                包.Dispose();
        }
    }
}
