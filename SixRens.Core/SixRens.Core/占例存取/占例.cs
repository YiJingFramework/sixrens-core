using SixRens.Core.占例存取.可序列化类型;
using SixRens.Core.壬式生成;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SixRens.Core.占例存取
{
    public sealed class 占例
    {
        public 壬式 壬式 { get; }
        public string 断语 { get; set; }

        internal 占例(壬式 壬式, string 断语 = "")
        {
            this.壬式 = 壬式;
            this.断语 = 断语;
        }

        public string 序列化()
        {
            return JsonSerializer.Serialize(new 可序列化占例(this));
        }

        public static 占例 反序列化(string s)
        {
            return (JsonSerializer.Deserialize<可序列化占例>(s) ?? new 可序列化占例()).转占例();
        }
    }
}
