using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Core.实体;
using SixRens.Core.扩展;
using System.Collections.ObjectModel;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core
{
    public sealed class 壬式
    {
        public I年月日时 年月日时 { get; }

        public 地支盘 地盘 { get; }
        public 地支盘 天盘 { get; }

        public I四课 四课 { get; }
        public I三传 三传 { get; }

        public I天将盘 天将盘 { get; }

        public I年命? 课主年命 { get; }
        public IReadOnlyList<I年命> 对象年命 { get; }

        public IReadOnlyList<I神煞> 神煞 { get; }
        public IReadOnlyList<I课体> 课体 { get; }

        public IReadOnlyList<I占断参考> 占断参考 { get; }

        public 壬式(I年月日时信息 年月日时,
            本命信息? 课主本命,
            IEnumerable<本命信息> 对象本命,
            I地盘插件 地盘插件,
            I天盘插件 天盘插件,
            I四课插件 四课插件,
            I三传插件 三传插件,
            I天将插件 天将插件,
            I年命插件 年命插件,
            IEnumerable<I神煞插件> 神煞插件,
            IEnumerable<I课体插件> 课体插件,
            IEnumerable<I参考插件> 参考插件)
        {
            this.年月日时 = 年月日时.生成年月日时();

            var 基础盘 = new 地支盘();

            try
            {
                地盘 = 地盘插件.获取地盘(
                   this.年月日时, 基础盘);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取地盘失败：{异常.Message}", 异常);
            }

            try
            {
                天盘 = 天盘插件.获取天盘(
                    this.年月日时, 基础盘, 地盘);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取天盘失败：{异常.Message}", 异常);
            }

            try
            {
                四课 = 四课插件.获取四课(
                    this.年月日时, 基础盘, 地盘, 天盘);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取四课失败：{异常.Message}", 异常);
            }

            try
            {
                三传 = 三传插件.获取三传(
                    this.年月日时, 基础盘, 地盘, 天盘, 四课);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取三传失败：{异常.Message}", 异常);
            }

            try
            {
                天将盘 = 天将插件.获取天将盘(
                    this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取天将失败：{异常.Message}", 异常);
            }

            if (课主本命 is not null)
            {
                I年命 年命;
                try
                {
                    年命 = 年命插件.获取年命(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        课主本命.性别, 课主本命.本命);
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取课主年命失败：{异常.Message}", 异常);
                }

                课主年命 = new 年命信息(年命.性别, 年命.本命, 年命.行年);
            }
            // else
            //    this.课主本命 = null;
            IEnumerable<I年命> 对象年命;
            try
            {
                对象年命 =
                    from 本命 in 对象本命
                    let 年命 = 年命插件.获取年命(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        本命.性别, 本命.本命)
                    select new 年命信息(年命.性别, 年命.本命, 年命.行年);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取对象年命失败：{异常.Message}", 异常);
            }
            this.对象年命 = Array.AsReadOnly(对象年命.ToArray());

            Dictionary<string, List<EarthlyBranch>> 神煞字典 = new();
            foreach (var 插件 in 神煞插件)
            {
                IEnumerable<I神煞> 插件神煞表;
                try
                {
                    插件神煞表 = 插件.获取神煞(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        课主年命, this.对象年命);
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取神煞失败（插件：{插件.插件识别码}）：{异常.Message}", 异常);
                }
                foreach (var 神煞 in 插件神煞表)
                {
                    if (!神煞字典.ContainsKey(神煞.神煞名))
                        神煞字典.Add(神煞.神煞名, new());
                    if (神煞.所在神 is not null)
                        神煞字典[神煞.神煞名].AddRange(神煞.所在神);
                }
            }
            var 神煞表 = new List<神煞>(神煞字典.Count);
            foreach (var (神煞名, 所在神) in 神煞字典)
            {
                var 在神 = 所在神.ToArray();
                if (在神.Length > 0)
                    神煞表.Add(new 神煞(神煞名, Array.AsReadOnly(在神)));
            }
            神煞 = new ReadOnlyCollection<神煞>(神煞表);

            HashSet<I课体> 课体 = new();
            foreach (var 插件 in 课体插件)
            {
                IEnumerable<I课体> 插件课体;
                try
                {
                    插件课体 = 插件.识别课体(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        课主年命, this.对象年命, 神煞);
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取课体失败（插件：{插件.插件识别码}）：{异常.Message}", 异常);
                }
                foreach (var 单课体 in 插件课体)
                    _ = 课体.Add(new 课体(单课体.课体名));
            }
            this.课体 = Array.AsReadOnly(课体.ToArray());


            HashSet<I占断参考> 占断参考 = new();
            foreach (var 插件 in 参考插件)
            {
                IEnumerable<I占断参考> 插件参考;
                try
                {
                    插件参考 = 插件.生成占断参考(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        课主年命, this.对象年命, 神煞, this.课体);
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取参考失败（插件：{插件.插件识别码}）：{异常.Message}", 异常);
                }
                foreach (var 单参考 in 插件参考)
                    _ = 占断参考.Add(new 占断参考(单参考.题目, 单参考.内容));
            }
            this.占断参考 = Array.AsReadOnly(占断参考.ToArray());
        }

        public 天将 取所乘将(EarthlyBranch 上者)
        {
            return 天将盘.取乘将(上者);
        }
        public 天将 取临我将(EarthlyBranch 下者)
        {
            return 天将盘.取乘将(取所乘神(下者));
        }
        public EarthlyBranch 取所乘神(EarthlyBranch 下者)
        {
            return 天盘.获取同位支(地盘, 下者);
        }
        public EarthlyBranch 取所临神(EarthlyBranch 上者)
        {
            return 地盘.获取同位支(天盘, 上者);
        }
    }
}
