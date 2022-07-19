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

        public I地盘插件 所用地盘插件 { get; }
        public I天盘插件 所用天盘插件 { get; }
        public I四课插件 所用四课插件 { get; }
        public I三传插件 所用三传插件 { get; }
        public I天将插件 所用天将插件 { get; }
        public I年命插件 所用年命插件 { get; }
        public IReadOnlyCollection<I神煞插件> 所用神煞插件 { get; }
        public IReadOnlyCollection<I课体插件> 所用课体插件 { get; }
        public IReadOnlyCollection<I参考插件> 所用参考插件 { get; }

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
            所用地盘插件 = 地盘插件;
            所用天盘插件 = 天盘插件;
            所用四课插件 = 四课插件;
            所用三传插件 = 三传插件;
            所用天将插件 = 天将插件;
            所用年命插件 = 年命插件;
            所用神煞插件 = Array.AsReadOnly(神煞插件.ToArray());
            所用课体插件 = Array.AsReadOnly(课体插件.ToArray());
            所用参考插件 = Array.AsReadOnly(参考插件.ToArray());

            this.年月日时 = 年月日时.生成年月日时();

            var 基础盘 = new 地支盘();

            try
            {
                地盘 = 所用地盘插件.获取地盘(
                   this.年月日时, 基础盘);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取地盘失败：{异常.Message}", 异常);
            }

            try
            {
                天盘 = 所用天盘插件.获取天盘(
                    this.年月日时, 基础盘, 地盘);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取天盘失败：{异常.Message}", 异常);
            }

            try
            {
                var 四课 = 所用四课插件.获取四课(
                    this.年月日时, 基础盘, 地盘, 天盘);
                this.四课 = new 四课(四课.日, 四课.日阳, 四课.日阴, 四课.辰, 四课.辰阳, 四课.辰阴);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取四课失败：{异常.Message}", 异常);
            }

            try
            {
                var 三传 = 所用三传插件.获取三传(
                    this.年月日时, 基础盘, 地盘, 天盘, 四课);
                this.三传 = new 三传(三传.初传, 三传.中传, 三传.末传);
            }
            catch (起课失败异常 异常)
            {
                throw new 起课失败异常($"获取三传失败：{异常.Message}", 异常);
            }

            try
            {
                var 天将盘 = 所用天将插件.获取天将盘(
                    this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传);
                this.天将盘 = new 天将盘(天将盘);
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
                    年命 = 所用年命插件.获取年命(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        课主本命.性别, 课主本命.本命);
                    课主年命 = new 年命信息(年命.性别, 年命.本命, 年命.行年);
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取课主年命失败：{异常.Message}", 异常);
                }
            }
            // else
            //    this.课主本命 = null;

            IEnumerable<I年命> 对象年命;
            try
            {
                对象年命 =
                    from 本命 in 对象本命
                    let 年命 = 所用年命插件.获取年命(
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
            foreach (var 插件 in 所用神煞插件)
            {
                try
                {
                    foreach (var 神煞 in 插件.支持的神煞)
                    {
                        if (!神煞字典.ContainsKey(神煞.神煞名))
                            神煞字典.Add(神煞.神煞名, new(1));

                        var 神煞内容 = 插件.获取神煞(
                                this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                                课主年命, this.对象年命, 神煞);
                        神煞字典[神煞.神煞名].AddRange(神煞内容.所在神);
                    }
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取神煞失败（插件：{插件.插件识别码}）：{异常.Message}", 异常);
                }
            }

            var 神煞表 =
                from 神煞名所在神 in 神煞字典
                let 神煞名 = 神煞名所在神.Key
                let 所在神 = 神煞名所在神.Value
                where 所在神.Count > 0
                select new 神煞(神煞名, Array.AsReadOnly(所在神.ToArray()));
            神煞 = Array.AsReadOnly(神煞表.ToArray());

            HashSet<I课体> 课体 = new();
            foreach (var 插件 in 所用课体插件)
            {
                try
                {
                    var 插件课体 = 插件.识别课体(
                        this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                        课主年命, this.对象年命, 神煞);
                    foreach (var 单课体 in 插件课体)
                        _ = 课体.Add(new 课体(单课体.课体名));
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取课体失败（插件：{插件.插件识别码}）：{异常.Message}", 异常);
                }
            }
            this.课体 = Array.AsReadOnly(课体.ToArray());


            List<I占断参考> 占断参考 = new();
            foreach (var 插件 in 所用参考插件)
            {
                IEnumerable<占断参考> 插件参考;
                try
                {
                    插件参考 =
                        from 参考题目 in 插件.支持的占断参考
                        let 参考内容 = 插件.生成占断参考(
                            this.年月日时, 基础盘, 地盘, 天盘, 四课, 三传, 天将盘,
                            课主年命, this.对象年命, 神煞, this.课体, 参考题目)
                        where 参考内容.内容 is not null
                        select new 占断参考(参考题目.题目, 参考内容.内容);
                }
                catch (起课失败异常 异常)
                {
                    throw new 起课失败异常($"获取参考失败（插件：{插件.插件识别码}）：{异常.Message}", 异常);
                }
                占断参考.AddRange(插件参考);
            }
            this.占断参考 = new ReadOnlyCollection<I占断参考>(占断参考);
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
