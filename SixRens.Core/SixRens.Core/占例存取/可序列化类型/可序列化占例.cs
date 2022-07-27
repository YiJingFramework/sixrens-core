using SixRens.Core.占例存取.可序列化类型;
using SixRens.Core.壬式生成;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化占例
    {
        public 可序列化占例(占例 占例)
        {
            this.壬式 = new(占例.壬式);
            this.断语 = 占例.断语;
        }
        public 可序列化占例() { }

        public 可序列化壬式? 壬式 { get; init; }
        public string? 断语 { get; init; }

        public 占例 转占例()
        {
            return new 占例(
                (this.壬式 ?? new 可序列化壬式()).转壬式(),
                this.断语 ?? string.Empty);
        }
    }
}
