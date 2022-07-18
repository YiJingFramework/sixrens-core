using SixRens.Api;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Loader;
using System.Security;
using System.Text.Json;

namespace SixRens.Core.插件
{
    public sealed class 插件包
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

        public IReadOnlyCollection<I地盘插件> 地盘插件 { get; }
        public IReadOnlyCollection<I天盘插件> 天盘插件 { get; }
        public IReadOnlyCollection<I四课插件> 四课插件 { get; }
        public IReadOnlyCollection<I三传插件> 三传插件 { get; }
        public IReadOnlyCollection<I天将插件> 天将插件 { get; }
        public IReadOnlyCollection<I年命插件> 年命插件 { get; }
        public IReadOnlyCollection<I神煞插件> 神煞插件 { get; }
        public IReadOnlyCollection<I课体插件> 课体插件 { get; }
        public IReadOnlyCollection<I参考插件> 参考插件 { get; }
        public AssemblyLoadContext 插件包上下文 { get; }

        private static 可序列化插件包信息 获取并解析信息文件(ZipArchive 插件包)
        {
            ZipArchiveEntry 信息文件项;
            try
            {
                const string 插件包信息文件路径 = "plugin.json";
                信息文件项 = 插件包.Entries
                    .Where(项 => 项.FullName is 插件包信息文件路径)
                    .Single();
            }
            catch (InvalidOperationException e)
            {
                throw new 插件包读取异常("找不到插件包信息文件", e);
            }
            可序列化插件包信息? 可序列化信息文件;

            try
            {
                using var 信息文件流 = 信息文件项.Open();
                可序列化信息文件 = JsonSerializer.Deserialize<可序列化插件包信息>(信息文件流);
            }
            catch (IOException e)
            {
                throw new 插件包读取异常("插件包正在被其他应用访问", e);
            }
            catch (InvalidDataException e)
            {
                throw new 插件包读取异常("插件包格式不正确", e);
            }
            catch (JsonException e)
            {
                throw new 插件包读取异常("插件包信息文件格式不正确", e);
            }

            if (可序列化信息文件 is null)
                throw new 插件包读取异常(
                    "插件包信息文件格式不正确");
            if (可序列化信息文件.名称 is null)
                throw new 插件包读取异常(
                    $"插件包信息文件缺失必须的{nameof(可序列化插件包信息.名称)}信息");
            if (可序列化信息文件.版本号 is null)
                throw new 插件包读取异常(
                    $"插件包信息文件缺失必须的{nameof(可序列化插件包信息.版本号)}信息");
            if (可序列化信息文件.主程序集 is null)
                throw new 插件包读取异常(
                    $"插件包信息文件缺失必须的{nameof(可序列化插件包信息.主程序集)}信息");
            if (可序列化信息文件.插件类 is null)
                throw new 插件包读取异常(
                    $"插件包信息文件缺失必须的{nameof(可序列化插件包信息.插件类)}信息");
            return 可序列化信息文件;
        }

        private static (AssemblyLoadContext, Assembly? 主程序集)
            加载全部程序集(ZipArchive 插件包, string 主程序集路径)
        {
            AssemblyLoadContext 上下文 = new AssemblyLoadContext(null, true);
            Assembly? 主程序集 = null;
            try
            {
                const string 程序集后缀 = ".dll";
                foreach (var 项目 in 插件包.Entries
                    .Where(路径 => Path.GetExtension(路径.Name).ToLowerInvariant() is 程序集后缀))
                {
                    const string 六壬接口程序集名称 = "SixRens.Api";
                    const string 六壬接口程序集名称含后缀 = $"{六壬接口程序集名称}.dll";
                    if (项目.Name is 六壬接口程序集名称含后缀)
                        continue;
                    try
                    {
                        using var 项目流 = 项目.Open();
                        var 程序集 = 上下文.LoadFromStream(项目流);
                        if (主程序集 is null && 项目.FullName == 主程序集路径)
                            主程序集 = 程序集;
                        if (程序集.GetName().Name is 六壬接口程序集名称)
                        {
                            throw new 插件包读取异常(
                                $"插件包内容不正确，插件包不可包含被重命名过的{六壬接口程序集名称}");
                        }
                    }
                    catch (IOException e)
                    {
                        throw new 插件包读取异常("插件包正在被其他应用访问", e);
                    }
                    catch (InvalidDataException e)
                    {
                        throw new 插件包读取异常("插件包格式不正确", e);
                    }
                    catch (BadImageFormatException e)
                    {
                        throw new 插件包读取异常($"无法载入插件包中的程序集：{项目.FullName}", e);
                    }
                }
            }
            catch
            {
                上下文?.Unload();
                throw;
            }
            return (上下文, 主程序集);
        }

        private static I插件 实例化插件(Assembly 程序集, string 类名)
        {
            Type? 插件类;
            try
            {
                插件类 = 程序集.GetType(类名, false);
            }
            catch (ArgumentException e)
            {
                throw new 插件包读取异常($"找不到指定插件类：{类名}", e);
            }
            catch (FileNotFoundException e)
            {
                throw new 插件包读取异常($"指定插件类加载失败：{类名}", e);
            }
            catch (FileLoadException e)
            {
                throw new 插件包读取异常($"指定插件类加载失败：{类名}", e);
            }
            catch (BadImageFormatException e)
            {
                throw new 插件包读取异常($"指定插件类加载失败：{类名}", e);
            }
            if (插件类 is null)
                throw new 插件包读取异常($"找不到指定插件类：{类名}");

            var 构造函数 = 插件类.GetConstructor(Array.Empty<Type>());
            if (构造函数 is null)
                throw new 插件包读取异常($"指定插件类没有无参构造函数：{类名}");

            object 实例;
            try
            {
                实例 = 构造函数.Invoke(Array.Empty<object>());
            }
            catch (MemberAccessException e)
            {
                throw new 插件包读取异常($"指定插件无法通过无参构造函数创建实例：{类名}", e);
            }
            catch (TargetInvocationException e)
            {
                throw new 插件包读取异常($"实例化指定插件时发生异常：{类名}", e);
            }
            catch (NotSupportedException e)
            {
                throw new 插件包读取异常($"指定插件无法通过无参构造函数创建实例：{类名}", e);
            }
            catch (SecurityException e)
            {
                throw new 插件包读取异常($"指定插件无法通过无参构造函数创建实例：{类名}", e);
            }

            if (实例 is I插件 结果)
                return 结果;
            else
                throw new 插件包读取异常($"指定插件类没有实现插件接口：{类名}");
        }

        public 插件包(Stream 插件包流)
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

                名称 = 信息.名称;
                版本号 = 信息.版本号;
                网址 = 信息.网址;

                (插件包上下文, 主程序集) = 加载全部程序集(插件包, 信息.主程序集);
            }
            try
            {
                if (主程序集 is null)
                    throw new 插件包读取异常("没有找到主程序集");

                List<I地盘插件> 地盘插件 = new();
                List<I天盘插件> 天盘插件 = new();
                List<I四课插件> 四课插件 = new();
                List<I三传插件> 三传插件 = new();
                List<I天将插件> 天将插件 = new();
                List<I年命插件> 年命插件 = new();
                List<I神煞插件> 神煞插件 = new();
                List<I课体插件> 课体插件 = new();
                List<I参考插件> 参考插件 = new();
                foreach (var 插件类名 in 信息.插件类)
                {
                    if (插件类名 is null)
                        throw new 插件包读取异常(
                            $"插件包信息文件{nameof(可序列化插件包信息.插件类)}内容不正确，" +
                            $"不得有为 null 的项");

                    var 插件 = 实例化插件(主程序集, 插件类名);
                    if (插件.插件识别码 == Guid.Empty)
                        throw new 插件包读取异常(
                            $"此插件没有指定识别码：{插件.插件名}（{插件类名}）");

                    bool 已储存 = false;

                    if (插件 is I地盘插件 地盘)
                    {
                        地盘插件.Add(地盘);
                        已储存 = true;
                    }
                    if (插件 is I天盘插件 天盘)
                    {
                        天盘插件.Add(天盘);
                        已储存 = true;
                    }
                    if (插件 is I四课插件 四课)
                    {
                        四课插件.Add(四课);
                        已储存 = true;
                    }
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
                    if (插件 is I年命插件 年命)
                    {
                        年命插件.Add(年命);
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

                this.地盘插件 = Array.AsReadOnly(地盘插件.ToArray());
                this.天盘插件 = Array.AsReadOnly(天盘插件.ToArray());
                this.四课插件 = Array.AsReadOnly(四课插件.ToArray());
                this.三传插件 = Array.AsReadOnly(三传插件.ToArray());
                this.天将插件 = Array.AsReadOnly(天将插件.ToArray());
                this.年命插件 = Array.AsReadOnly(年命插件.ToArray());
                this.神煞插件 = Array.AsReadOnly(神煞插件.ToArray());
                this.课体插件 = Array.AsReadOnly(课体插件.ToArray());
                this.参考插件 = Array.AsReadOnly(参考插件.ToArray());
            }
            catch
            {
                插件包上下文.Unload();
                throw;
            }
        }
    }
}
