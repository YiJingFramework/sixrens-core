using SixRens.Api;
using SixRens.Api.实体;
using SixRens.Api.实体.壬式;
using SixRens.Api.实体.起课信息;
using SixRens.Core.占例存取;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 壬式
    {
        public 起课参数 起课参数 { get; }
        public 天地盘 天地盘 { get; }
        public 四课 四课 { get; }
        public 三传 三传 { get; }
        public 天将盘 天将盘 { get; }
        public IReadOnlyList<神煞> 神煞 { get; }
        public IReadOnlyList<课体> 课体 { get; }
        public IReadOnlyList<占断参考> 占断参考 { get; }

        internal 壬式(
            起课参数 起课参数,
            三传 三传, 
            去冗天将盘 天将盘,
            IEnumerable<神煞> 神煞,
            IEnumerable<课体> 课体,
            IEnumerable<占断参考> 占断参考)
        {
            this.起课参数 = 起课参数;
            this.天地盘 = new(起课参数.年月日时);
            this.四课 = new(起课参数.年月日时, this.天地盘);
            this.三传 = 三传;
            this.天将盘 = new(天将盘.所用插件, 天地盘, 天将盘);
            this.神煞 = Array.AsReadOnly(神煞.ToArray());
            this.课体 = Array.AsReadOnly(课体.ToArray());
            this.占断参考 = Array.AsReadOnly(占断参考.ToArray());
        }

        public 壬式(
            起课参数 起课参数,
            经过解析的预设 预设)
        {
            this.起课参数 = 起课参数;

            {
                this.天地盘 = new(起课参数.年月日时);
            }

            {
                this.四课 = new(起课参数.年月日时, this.天地盘);
            }

            {
                var 三传 = 预设.三传插件.获取三传(this.起课参数, this.天地盘, this.四课);
                this.三传 = new 三传(预设.三传插件.插件识别码, 三传);
            }

            {
                var 天将盘 = 预设.天将插件.获取天将盘(this.起课参数, this.天地盘, this.四课, this.三传);
                this.天将盘 = new 天将盘(预设.天将插件.插件识别码, this.天地盘, 天将盘);
            }

            {
                List<神煞> 神煞 = new(预设.启用的神煞和所属插件.Count);
                Dictionary<Guid, I神煞插件.I神煞内容提供器> 插件提供器表 = new();
                foreach (var 神煞和插件 in 预设.启用的神煞和所属插件)
                {
                    var 插件识别码 = 神煞和插件.插件.插件识别码;
                    if (!插件提供器表.TryGetValue(插件识别码, out var 提供器))
                    {
                        提供器 = 神煞和插件.插件.获取神煞内容提供器(
                            this.起课参数, this.天地盘, this.四课, this.三传, this.天将盘);
                        插件提供器表.Add(插件识别码, 提供器);
                    }
                    var 题目 = 神煞和插件.题目;
                    var 结果 = new 神煞(插件识别码, 题目, 提供器.取所在神(题目));
                    神煞.Add(结果);
                }
                this.神煞 = Array.AsReadOnly(神煞.ToArray());
            }

            {
                List<课体> 课体 = new(预设.启用的课体和所属插件.Count);
                Dictionary<Guid, I课体插件.I课体内容提供器> 插件提供器表 = new();
                foreach (var 课体和插件 in 预设.启用的课体和所属插件)
                {
                    var 插件识别码 = 课体和插件.插件.插件识别码;
                    if (!插件提供器表.TryGetValue(插件识别码, out var 提供器))
                    {
                        提供器 = 课体和插件.插件.获取课体内容提供器(
                            this.起课参数, this.天地盘, this.四课, this.三传, this.天将盘, this.神煞);
                        插件提供器表.Add(插件识别码, 提供器);
                    }
                    var 题目 = 课体和插件.题目;
                    var 结果 = new 课体(插件识别码, 题目, 提供器.属此课体(题目));
                    课体.Add(结果);
                }
                this.课体 = Array.AsReadOnly(课体.ToArray());
            }

            {
                var 参考 =
                    from 插件 in 预设.参考插件
                    let 所有参考 = 插件.生成占断参考(
                        this.起课参数, this.天地盘, this.四课, this.三传, this.天将盘, this.神煞, this.课体)
                    from 参考内容 in 所有参考
                    select new 占断参考(插件.插件识别码, 参考内容);
                this.占断参考 = Array.AsReadOnly(参考.ToArray());
            }
        }

        public 占例 创建占例()
        {
            return new 占例(this, string.Empty, null);
        }
    }
}
