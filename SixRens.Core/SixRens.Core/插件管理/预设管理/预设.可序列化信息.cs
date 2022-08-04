namespace SixRens.Core.插件管理.预设管理
{
    public sealed partial class 预设
    {
        internal sealed class 可序列化信息
        {
            public Guid? 三传插件 { get; set; }
            public Guid? 天将插件 { get; set; }
            public List<Guid>? 神煞插件 { get; set; }
            public List<Guid>? 课体插件 { get; set; }
            public List<Guid>? 参考插件 { get; set; }

            public List<实体题目和所属插件识别码>? 神煞启用 { get; set; }
            public List<实体题目和所属插件识别码>? 课体启用 { get; set; }
            public List<实体题目和所属插件识别码>? 神煞禁用 { get; set; }
            public List<实体题目和所属插件识别码>? 课体禁用 { get; set; }
        }
    }
}
