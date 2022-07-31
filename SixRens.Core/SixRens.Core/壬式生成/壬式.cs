using SixRens.Api.实体;
using SixRens.Core.占例存取;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理.预设管理;
using YiJingFramework.StemsAndBranches;
using static SixRens.Api.工具.天盘扩展;

namespace SixRens.Core.壬式生成
{
    public sealed class 壬式
    {
        public I年月日时 年月日时 { get; }

        public 地盘 地盘 { get; }
        public 天盘 天盘 { get; }

        private readonly 可逆天盘 可逆天盘;

        private readonly Dictionary<EarthlyBranch, IReadOnlyList<神煞>> 从地支查神煞表;
        private static Dictionary<EarthlyBranch, IReadOnlyList<神煞>> 制从地支查神煞表(
            IReadOnlyList<神煞> 神煞)
        {
            var 从地支查神煞表 = new List<神煞>[12] {
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
                new List<神煞>(),
            };

            foreach (var 煞 in 神煞)
            {
                foreach (var 神 in 煞.所在神)
                {
                    从地支查神煞表[神.Index - 1].Add(煞);
                }
            }
            return 从地支查神煞表
                .Select((s, i) => (branch: new EarthlyBranch(i + 1), s: s.ToArray()))
                .ToDictionary(bs => bs.branch,
                bs => (IReadOnlyList<神煞>)Array.AsReadOnly(bs.s));
        }

        public 四课 四课 { get; }
        public 三传 三传 { get; }

        public 天将盘 天将盘 { get; }

        public 年命? 课主年命 { get; }
        public IReadOnlyList<年命> 对象年命 { get; }

        public IReadOnlyList<神煞> 神煞 { get; }
        public IReadOnlyList<课体> 课体 { get; }

        public IReadOnlyList<占断参考> 占断参考 { get; }

        internal 壬式(
            I年月日时 年月日时,
            地盘 地盘, 天盘 天盘,
            四课 四课, 三传 三传,
            天将盘 天将盘,
            年命? 课主年命, IEnumerable<年命> 对象年命,
            IEnumerable<神煞> 神煞,
            IEnumerable<课体> 课体,
            IEnumerable<占断参考> 占断参考)
        {
            this.年月日时 = 年月日时;
            this.地盘 = 地盘;
            this.天盘 = 天盘;
            this.可逆天盘 = this.天盘.可逆化();
            this.四课 = 四课;
            this.三传 = 三传;
            this.天将盘 = 天将盘;
            this.课主年命 = 课主年命;
            this.对象年命 = Array.AsReadOnly(对象年命.ToArray());
            this.神煞 = Array.AsReadOnly(神煞.ToArray());
            this.从地支查神煞表 = 制从地支查神煞表(this.神煞);
            this.课体 = Array.AsReadOnly(课体.ToArray());
            this.占断参考 = Array.AsReadOnly(占断参考.ToArray());
        }

        public 壬式(
            I年月日时信息 年月日时,
            本命信息? 课主本命,
            IEnumerable<本命信息> 对象本命,
            经过解析的预设 预设)
        {
            Guid 壬式识别码 = 壬式识别码生成器.新识别码;

            this.年月日时 = 年月日时.生成年月日时();

            {
                var 地盘 = 预设.地盘插件.获取地盘(壬式识别码, this.年月日时);
                this.地盘 = new 地盘(预设.地盘插件.插件识别码, 地盘);
            }

            {
                var 天盘 = 预设.天盘插件.获取天盘(壬式识别码, this.年月日时, this.地盘);
                this.天盘 = new 天盘(预设.天盘插件.插件识别码, 天盘);
                this.可逆天盘 = this.天盘.可逆化();
            }

            {
                var 四课 = 预设.四课插件.获取四课(壬式识别码, this.年月日时, this.地盘, this.天盘);
                this.四课 = new 四课(预设.四课插件.插件识别码, 四课);
            }

            {
                var 三传 = 预设.三传插件.获取三传(壬式识别码, this.年月日时, this.地盘, this.天盘, this.四课);
                this.三传 = new 三传(预设.三传插件.插件识别码, 三传);
            }

            {
                var 天将盘 = 预设.天将插件.获取天将盘(壬式识别码,
                    this.年月日时, this.地盘, this.天盘, this.四课, this.三传);
                this.天将盘 = new 天将盘(预设.天将插件.插件识别码, 天将盘);
            }

            if (课主本命 is not null)
            {
                I年命 年命;
                年命 = 预设.年命插件.获取年命(壬式识别码,
                    this.年月日时, this.地盘, this.天盘, this.四课, this.三传, this.天将盘,
                    课主本命.性别, 课主本命.本命);
                this.课主年命 = new 年命(预设.年命插件.插件识别码, 年命);
            }

            {
                var 对象年命 =
                    from 本命 in 对象本命
                    let 年命 = 预设.年命插件.获取年命(壬式识别码,
                        this.年月日时, this.地盘, this.天盘, this.四课, this.三传, this.天将盘,
                        本命.性别, 本命.本命)
                    select new 年命(预设.年命插件.插件识别码, 年命);
                this.对象年命 = Array.AsReadOnly(对象年命.ToArray());
            }

            {
                var 神煞 =
                    from 神煞和插件 in 预设.启用的神煞和所属插件
                    let 插件 = 神煞和插件.插件
                    let 神煞名 = 神煞和插件.题目
                    let 神煞内容 = 插件.获取神煞(壬式识别码,
                        this.年月日时, this.地盘, this.天盘, this.四课, this.三传,
                        this.天将盘, this.课主年命, this.对象年命,
                        神煞名)
                    select new 神煞(插件.插件识别码, 神煞名, 神煞内容);
                this.神煞 = Array.AsReadOnly(神煞.ToArray());
                this.从地支查神煞表 = 制从地支查神煞表(this.神煞);
            }

            {
                var 课体 =
                    from 课体和插件 in 预设.启用的课体和所属插件
                    let 插件 = 课体和插件.插件
                    let 课体名 = 课体和插件.题目
                    let 课体内容 = 插件.识别课体(壬式识别码,
                        this.年月日时, this.地盘, this.天盘, this.四课, this.三传,
                        this.天将盘, this.课主年命, this.对象年命, this.神煞,
                        课体名)
                    select new 课体(插件.插件识别码, 课体名, 课体内容);
                this.课体 = Array.AsReadOnly(课体.ToArray());
            }

            {
                var 参考 =
                    from 插件 in 预设.参考插件
                    from 参考题目 in 插件.支持的占断参考
                    let 题目字符串 = 参考题目.题目
                    let 参考内容 = 插件.生成占断参考(壬式识别码,
                        this.年月日时, this.地盘, this.天盘, this.四课, this.三传,
                        this.天将盘, this.课主年命, this.对象年命, this.神煞, this.课体,
                        题目字符串)
                    select new 占断参考(插件.插件识别码, 题目字符串, 参考内容);
                this.占断参考 = Array.AsReadOnly(参考.ToArray());
            }
        }

        public 天将 取乘将(EarthlyBranch 天神)
        {
            return this.天将盘.取乘将(天神);
        }
        public 天将 取临将(EarthlyBranch 地盘支)
        {
            return this.天将盘.取乘将(this.取上神(地盘支));
        }
        public EarthlyBranch 取上神(EarthlyBranch 地盘支)
        {
            return this.天盘.取天神(地盘支);
        }
        public EarthlyBranch? 取临地(EarthlyBranch 天神)
        {
            return this.可逆天盘.取临地(天神);
        }
        public EarthlyBranch 取旬遁某干之支(HeavenlyStem 天干)
        {
            return this.年月日时.旬所在.获取对应地支(天干);
        }
        public HeavenlyStem? 取某支旬遁之干(EarthlyBranch 地支)
        {
            return this.年月日时.旬所在.获取对应天干(地支);
        }
        public (EarthlyBranch, EarthlyBranch) 取旬内空亡二支()
        {
            return (this.年月日时.旬所在.空亡一, this.年月日时.旬所在.空亡二);
        }
        public IReadOnlyList<神煞> 取神煞(EarthlyBranch 地支)
        {
            return this.从地支查神煞表[地支];
        }

        public 占例 创建占例()
        {
            return new 占例(this, string.Empty, null);
        }
    }
}
