using System.IO.Compression;
using System.Reflection;
using SixRens.Api;
using SixRens.Api.工具;
using 测试用插件包;

AssemblyName 主程序集名 = Assembly.GetExecutingAssembly().GetName();

string[] 要打包的程序集 = new[]
{
    $"{主程序集名.Name}.dll"
};

插件包信息 插件包信息 = new(
    名称: $"{主程序集名.Name}",
    版本号: $"{主程序集名.Version?.ToString(3)}",
    网址: "一个网址",
    主程序集: $"{主程序集名.Name}.dll",
    插件类: Assembly.GetExecutingAssembly().GetTypes().Where(type=>type.IsAssignableTo(typeof(I插件)))
    );


// 下面开始是打包的过程，一般不需作修改
var 文件信息 = new FileInfo($"{插件包信息.名称}-{插件包信息.版本号}.srspg");

using var 文件 = new FileStream(文件信息.FullName, FileMode.Create);
using var 压缩包 = new ZipArchive(文件, ZipArchiveMode.Create);

using (var 信息项 =
    new StreamWriter(压缩包.CreateEntry("plugin.json").Open(), leaveOpen: false))
{
    信息项.Write(插件包信息.生成插件包信息文件内容());
}

foreach (var 程序集 in 要打包的程序集)
    _ = 压缩包.CreateEntryFromFile(程序集, 程序集);

Console.WriteLine($"已打包到 {文件信息.FullName}");