using SixRens.Api;

namespace SixRens.Core.插件管理.预设管理
{
    public sealed partial class 经过解析的预设
    {
        public 经过解析的预设(
            I地盘插件 地盘插件,
            I天盘插件 天盘插件,
            I四课插件 四课插件,
            I三传插件 三传插件,
            I天将插件 天将插件,
            I年命插件 年命插件,
            IEnumerable<实体题目和所属插件<I神煞插件>> 启用的神煞和所属插件,
            IEnumerable<实体题目和所属插件<I课体插件>> 启用的课体和所属插件,
            IEnumerable<I参考插件> 参考插件) : this(
                地盘插件, 天盘插件, 四课插件, 三传插件, 天将插件, 年命插件,
                启用的神煞和所属插件, false, false, false,
                启用的课体和所属插件, false, false, false,
                参考插件)
        { }

        internal 经过解析的预设(
            I地盘插件 地盘插件,
            I天盘插件 天盘插件,
            I四课插件 四课插件,
            I三传插件 三传插件,
            I天将插件 天将插件,
            I年命插件 年命插件,
            IEnumerable<实体题目和所属插件<I神煞插件>> 启用的神煞和所属插件,
            bool 存在未显式指定的神煞,
            bool 存在指定启用但未找到的神煞,
            bool 存在同时指定了启用和禁用的神煞,
            IEnumerable<实体题目和所属插件<I课体插件>> 启用的课体和所属插件,
            bool 存在未显式指定的课体,
            bool 存在指定启用但未找到的课体,
            bool 存在同时指定了启用和禁用的课体,
            IEnumerable<I参考插件> 参考插件)
        {
            this.地盘插件 = 地盘插件;
            this.天盘插件 = 天盘插件;
            this.四课插件 = 四课插件;
            this.三传插件 = 三传插件;
            this.天将插件 = 天将插件;
            this.年命插件 = 年命插件;
            this.启用的神煞和所属插件 = Array.AsReadOnly(启用的神煞和所属插件.ToArray());
            this.存在未显式指定的神煞 = 存在未显式指定的神煞;
            this.存在指定启用但未找到的神煞 = 存在指定启用但未找到的神煞;
            this.存在同时指定了启用和禁用的神煞 = 存在同时指定了启用和禁用的神煞;
            this.启用的课体和所属插件 = Array.AsReadOnly(启用的课体和所属插件.ToArray());
            this.存在未显式指定的课体 = 存在未显式指定的课体;
            this.存在指定启用但未找到的课体 = 存在指定启用但未找到的课体;
            this.存在同时指定了启用和禁用的课体 = 存在同时指定了启用和禁用的课体;
            this.参考插件 = Array.AsReadOnly(参考插件.ToArray());
        }

        public I地盘插件 地盘插件 { get; }
        public I天盘插件 天盘插件 { get; }
        public I四课插件 四课插件 { get; }
        public I三传插件 三传插件 { get; }
        public I天将插件 天将插件 { get; }
        public I年命插件 年命插件 { get; }
        public IReadOnlyList<实体题目和所属插件<I神煞插件>> 启用的神煞和所属插件 { get; }
        public bool 存在未显式指定的神煞 { get; }
        public bool 存在指定启用但未找到的神煞 { get; }
        public bool 存在同时指定了启用和禁用的神煞 { get; }
        public IReadOnlyList<实体题目和所属插件<I课体插件>> 启用的课体和所属插件 { get; }
        public bool 存在未显式指定的课体 { get; }
        public bool 存在指定启用但未找到的课体 { get; }
        public bool 存在同时指定了启用和禁用的课体 { get; }
        public IReadOnlyList<I参考插件> 参考插件 { get; }
    }
}
