using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.实体;
using YiJingFramework.StemsAndBranches;
using SixRens.扩展;

namespace SixRens
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
            IEnumerable<I课体插件> 课体插件)
        {
            this.年月日时 = 年月日时.生成年月日时();

            var 基础盘 = new 地支盘();

            this.地盘 = 地盘插件.获取地盘(
               this.年月日时, 基础盘);
            this.天盘 = 天盘插件.获取天盘(
                this.年月日时, 基础盘, this.地盘);

            this.四课 = 四课插件.获取四课(
                this.年月日时, 基础盘, this.地盘, this.天盘);
            this.三传 = 三传插件.获取三传(
                this.年月日时, 基础盘, this.地盘, this.天盘, this.四课);

            this.天将盘 = 天将插件.获取天将盘(
                this.年月日时, 基础盘, this.地盘, this.天盘, this.四课, this.三传);

            if (课主本命 is not null)
            {
                var 年命 = 年命插件.获取年命(
                    this.年月日时, 基础盘, this.地盘, this.天盘, this.四课, this.三传, this.天将盘,
                    课主本命.性别, 课主本命.本命);
                this.课主年命 = new 年命信息(年命.性别, 年命.本命, 年命.行年);
            }
            // else
            //    this.课主本命 = null;


            var 对象年命 =
                from 本命 in 对象本命
                let 年命 = 年命插件.获取年命(
                    this.年月日时, 基础盘, this.地盘, this.天盘, this.四课, this.三传, this.天将盘,
                    本命.性别, 本命.本命)
                select new 年命信息(年命.性别, 年命.本命, 年命.行年);
            this.对象年命 = Array.AsReadOnly(对象年命.ToArray());

            Dictionary<string, List<EarthlyBranch>> 神煞字典 = new();
            foreach (var 插件 in 神煞插件)
            {
                var 插件神煞表 = 插件.获取神煞(
                    this.年月日时, 基础盘, this.地盘, this.天盘, this.四课, this.三传, this.天将盘,
                    this.课主年命, this.对象年命);
                foreach (var 神煞 in 插件神煞表)
                {
                    if (!神煞字典.ContainsKey(神煞.神煞名))
                        神煞字典.Add(神煞.神煞名, new());
                    神煞字典[神煞.神煞名].AddRange(神煞.所在神);
                }
            }
            var 神煞表 = new List<神煞>(神煞字典.Count);
            foreach (var (神煞名, 所在神) in 神煞字典)
            {
                神煞表.Add(new 神煞(神煞名, Array.AsReadOnly(所在神.ToArray())));
            }
            this.神煞 = Array.AsReadOnly(神煞表.ToArray());

            var 课体 = 课体插件.SelectMany(
                插件 => 插件.识别课体(
                    this.年月日时, 基础盘, this.地盘, this.天盘, this.四课, this.三传, this.天将盘,
                    this.课主年命, this.对象年命, this.神煞
                )).DistinctBy(课体 => 课体.课体名);
            this.课体 = Array.AsReadOnly(课体.ToArray());
        }
    }
}
