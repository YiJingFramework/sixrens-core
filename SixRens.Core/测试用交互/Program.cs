using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;

namespace 测试用交互
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var 储存器 = new 储存器("储存");
            using var cjb = 插件包管理器.创建插件包管理器(储存器).AsTask().Result;
            var ys = 预设管理器.创建预设管理器(储存器).AsTask().Result;
            Application.Run(new 主窗体(cjb, ys));
        }
    }
}