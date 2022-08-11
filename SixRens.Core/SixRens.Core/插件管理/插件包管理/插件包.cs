using SixRens.Api;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.Json;

namespace SixRens.Core.插件管理.插件包管理
{
    public sealed class 插件包 : IDisposable
    {
        private sealed record 可序列化插件包信息
        {
            public string? 名称 { get; init; }
            public string? 版本号 { get; init; }
            public string? 网址 { get; init; }
            public string? 主程序集 { get; init; }
            public string?[]? 插件类 { get; init; }
        }

        public string 名称 { get; }
        public string 版本号 { get; }
        public string? 网址 { get; }

        /// <summary>
        /// 为了能够先加载插件包，随后再根据保存结果生成识别码，故设置为 internal set ，不应该在其他位置修改它。
        /// </summary>
        public string? 本地识别码 { get; internal set; }

        public IReadOnlyList<I三传插件> 三传插件 { get; }
        public IReadOnlyList<I天将插件> 天将插件 { get; }
        public IReadOnlyList<I神煞插件> 神煞插件 { get; }
        public IReadOnlyList<I课体插件> 课体插件 { get; }
        public IReadOnlyList<I参考插件> 参考插件 { get; }

        private static 可序列化插件包信息 获取并解析信息文件(ZipArchive 插件包)
        {
            const string 插件包信息文件路径 = "plugin.json";
            var 可能的信息文件项 = 插件包.Entries
                .Where(项 => 项.FullName is 插件包信息文件路径).ToArray();
            if (可能的信息文件项.Length is 0)
                throw new 插件包读取异常("找不到插件包信息文件");
            var 信息文件项 = 可能的信息文件项[0];

            可序列化插件包信息? 可序列化信息文件;
            try
            {
                using var 信息文件流 = 信息文件项.Open();
                可序列化信息文件 = JsonSerializer.Deserialize<可序列化插件包信息>(信息文件流);
            }
            catch (JsonException e)
            {
                throw new 插件包读取异常("插件包信息文件格式不正确。", e);
            }
            catch (Exception e)
            {
                throw new 插件包读取异常("无法加载插件包信息文件。", e);
            }

            if (可序列化信息文件 is null ||
                可序列化信息文件.名称 is null ||
                可序列化信息文件.版本号 is null ||
                可序列化信息文件.主程序集 is null ||
                可序列化信息文件.插件类 is null)
                throw new 插件包读取异常("插件包信息文件格式不正确。");
            return 可序列化信息文件;
        }

        private static (AssemblyLoadContext, Assembly? 主程序集)
            加载全部程序集(ZipArchive 插件包, string 主程序集路径)
        {
            AssemblyLoadContext 上下文 = new AssemblyLoadContext(null);
            Assembly? 主程序集 = null;
            const string 程序集后缀 = ".dll";
            foreach (var 项目 in 插件包.Entries
                .Where(路径 => Path.GetExtension(路径.Name).ToLowerInvariant() is 程序集后缀))
            {
                try
                {
                    using var 解压流 = new MemoryStream();
                    using (var 项目流 = 项目.Open())
                        项目流.CopyTo(解压流);
                    解压流.Position = 0;

                    var 程序集 = 上下文.LoadFromStream(解压流);
                    if (主程序集 is null && 项目.FullName == 主程序集路径)
                        主程序集 = 程序集;
                }
                catch (Exception e)
                {
                    throw new 插件包读取异常($"无法载入插件包中的程序集：{项目.FullName}", e);
                }
            }
            return (上下文, 主程序集);
        }

        private static I插件 实例化插件(Assembly 程序集, string 类名)
        {
            Type? 插件类;
            try
            {
                插件类 = 程序集.GetType(类名, true);
            }
            catch (Exception e)
            {
                throw new 插件包读取异常($"指定插件类加载失败：{类名}", e);
            }

            Debug.Assert(插件类 is not null);

            var 构造函数 = 插件类.GetConstructor(Array.Empty<Type>());
            if (构造函数 is null)
                throw new 插件包读取异常($"指定插件类没有无参构造函数：{类名}");

            object 实例;
            try
            {
                实例 = 构造函数.Invoke(Array.Empty<object>());
            }
            catch (Exception e)
            {
                throw new 插件包读取异常($"实例化指定插件类时发生异常：{类名}", e);
            }

            if (实例 is I插件 结果)
                return 结果;
            else
                throw new 插件包读取异常($"无法将指定插件类的实例转化为接口{nameof(I插件)}：{类名}");
        }

        public void Dispose()
        {
            foreach (var 插件 in 三传插件)
            {
                if (插件 is IDisposable 可释放)
                    可释放.Dispose();
            }
            foreach (var 插件 in 天将插件)
            {
                if (插件 is IDisposable 可释放)
                    可释放.Dispose();
            }
            foreach (var 插件 in 神煞插件)
            {
                if (插件 is IDisposable 可释放)
                    可释放.Dispose();
            }
            foreach (var 插件 in 课体插件)
            {
                if (插件 is IDisposable 可释放)
                    可释放.Dispose();
            }
            foreach (var 插件 in 参考插件)
            {
                if (插件 is IDisposable 可释放)
                    可释放.Dispose();
            }
        }

        internal 插件包(Stream 插件包流)
        {
            ZipArchive 插件包;
            try
            {
                插件包 = new ZipArchive(插件包流, ZipArchiveMode.Read, true);
            }
            catch (InvalidDataException e)
            {
                throw new 插件包读取异常("插件包格式不正确", e);
            }
            Assembly? 主程序集;
            可序列化插件包信息 信息;
            using (插件包)
            {
                信息 = 获取并解析信息文件(插件包);
                Debug.Assert(信息.名称 is not null);
                Debug.Assert(信息.版本号 is not null);
                Debug.Assert(信息.主程序集 is not null);
                Debug.Assert(信息.插件类 is not null);

                this.名称 = 信息.名称;
                this.版本号 = 信息.版本号;
                this.网址 = 信息.网址;

                (_, 主程序集) = 加载全部程序集(插件包, 信息.主程序集);
            }
            if (主程序集 is null)
                throw new 插件包读取异常("没有找到主程序集");

            List<I三传插件> 三传插件 = new(2);
            List<I天将插件> 天将插件 = new(1);
            List<I神煞插件> 神煞插件 = new(2);
            List<I课体插件> 课体插件 = new(2);
            List<I参考插件> 参考插件 = new();
            foreach (var 插件类名 in 信息.插件类)
            {
                if (插件类名 is null)
                    throw new 插件包读取异常($"插件包信息文件格式不正确");

                var 插件 = 实例化插件(主程序集, 插件类名);
                if (插件.插件识别码 == Guid.Empty)
                    throw new 插件包读取异常(
                        $"此插件没有指定识别码：{插件.插件名}（{插件类名}）");

                bool 已储存 = false;

                if (插件 is I三传插件 三传)
                {
                    三传插件.Add(三传);
                    已储存 = true;
                }
                if (插件 is I天将插件 天将)
                {
                    天将插件.Add(天将);
                    已储存 = true;
                }
                if (插件 is I神煞插件 神煞)
                {
                    神煞插件.Add(神煞);
                    已储存 = true;
                }
                if (插件 is I课体插件 课体)
                {
                    课体插件.Add(课体);
                    已储存 = true;
                }
                if (插件 is I参考插件 参考)
                {
                    参考插件.Add(参考);
                    已储存 = true;
                }

                if (!已储存)
                    throw new 插件包读取异常(
                        $"此插件不属于任何具体的插件类型：{插件.插件名}（{插件.插件识别码}）");
            }

            this.三传插件 = Array.AsReadOnly(三传插件.ToArray());
            this.天将插件 = Array.AsReadOnly(天将插件.ToArray());
            this.神煞插件 = Array.AsReadOnly(神煞插件.ToArray());
            this.课体插件 = Array.AsReadOnly(课体插件.ToArray());
            this.参考插件 = Array.AsReadOnly(参考插件.ToArray());
        }
    }
}
