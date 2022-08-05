using SixRens.Api.实体;
using SixRens.Api.实体.起课信息;
using SixRens.Core.壬式生成;
using System.Text.Json.Serialization;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化年命
    {
        public 可序列化年命(年命 年命)
        {
            this.性别 = 年命.性别.为男性;
            this.本命 = 年命.本命.Index;
            this.行年 = 年命.行年.Index;
        }
        public 可序列化年命() { }
        public bool 性别 { get; init; }
        public int 本命 { get; init; }
        public int 行年 { get; init; }

        public 年命 转年命()
        {
            return new 年命(new(this.性别), new(this.本命), new EarthlyBranch(this.行年));
        }
    }
}