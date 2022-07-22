using SixRens.Core;

namespace 测试用交互
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new 主窗体(new 核心(new DirectoryInfo("插件包"), new DirectoryInfo("预设"))));
        }
    }
}