using SixRens.Api.实体;
using SixRens.Api.工具;
using SixRens.Core.占例存取.可序列化类型;
using SixRens.Core.名称转换;
using SixRens.Core.壬式生成;
using System.Text;
using System.Text.Json;
using YiJingFramework.StemsAndBranches;

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

        private const string 空格 = "　";

        private void 追加年月日时(StringBuilder builder)
        {
            _ = builder
                .AppendLine(
                $"{this.壬式.年月日时.年干:C}{this.壬式.年月日时.年支:C}年{空格}" +
                $"{this.壬式.年月日时.月干:C}{this.壬式.年月日时.月支:C}月{空格}" +
                $"{this.壬式.年月日时.日干:C}{this.壬式.年月日时.日支:C}日{空格}" +
                $"{this.壬式.年月日时.时干:C}{this.壬式.年月日时.时支:C}时{空格}" +
                $"{this.壬式.年月日时.月将:C}将")
                .AppendLine(
                $"{this.壬式.年月日时.旬所在.旬首干:C}{this.壬式.年月日时.旬所在.旬首支:C}旬{空格}" +
                $"{this.壬式.年月日时.旬所在.空亡一:C}{this.壬式.年月日时.旬所在.空亡二:C}空")
                .AppendLine();
        }

        private void 追加年命(StringBuilder builder)
        {
            string 转字符串(I年命 年命)
            {
                return $"{年命.本命:C}命{年命.行年:C}年{(年命.性别.IsYang ? '男' : '女')}";
            }

            bool 有内容 = false;
            if (this.壬式.课主年命 is not null)
            {
                有内容 = true;
                _ = builder.AppendLine($"课主：{转字符串(this.壬式.课主年命)}");
            }
            if (this.壬式.对象年命.Count != 0)
            {
                有内容 = true;
                var 年命字符串表 = this.壬式.对象年命.Select(年命 => 转字符串(年命));
                _ = builder.AppendLine($"对象：{string.Join(空格, 年命字符串表)}");
            }
            if (有内容)
                _ = builder.AppendLine();
        }

        private void 追加三传(StringBuilder builder)
        {
            string 生成字符串(EarthlyBranch 支)
            {
                var 旬 = this.壬式.年月日时.旬所在;
                var 落空 = 旬.获取对应天干(this.壬式.取临地(支)).HasValue ? $"{空格}{空格}" : "落空";
                var 六亲 = this.壬式.年月日时.日干.判断六亲(支);
                var 遁干 = 旬.获取对应天干(支)?.ToString("C") ?? 空格;
                var 天将 = this.壬式.取乘将(支).简称();
                return $"{落空}{空格}{六亲}{空格}{遁干}{支:C}{天将}";
            }

            _ = builder
                .AppendLine(生成字符串(this.壬式.三传.初传))
                .AppendLine(生成字符串(this.壬式.三传.中传))
                .AppendLine(生成字符串(this.壬式.三传.末传))
                .AppendLine();
        }

        private void 追加四课(StringBuilder builder)
        {
            _ = builder.AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{this.壬式.取乘将(this.壬式.四课.辰阴).简称()}" +
                $"{this.壬式.取乘将(this.壬式.四课.辰阳).简称()}" +
                $"{this.壬式.取乘将(this.壬式.四课.日阴).简称()}" +
                $"{this.壬式.取乘将(this.壬式.四课.日阳).简称()}")
                .AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{this.壬式.四课.辰阴:C}{this.壬式.四课.辰阳:C}" +
                $"{this.壬式.四课.日阴:C}{this.壬式.四课.日阳:C}")
                .AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{this.壬式.四课.辰阳:C}{this.壬式.四课.辰:C}" +
                $"{this.壬式.四课.日阳:C}{this.壬式.四课.日:C}")
                .AppendLine();
        }

        private void 追加天盘(StringBuilder builder)
        {
            var 天盘 = Enumerable.Range(1, 12)
                .ToDictionary(i => i, i => this.壬式.取上神(this.壬式.地盘.取地支(new(i))));

            _ = builder
                .AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{this.壬式.取乘将(天盘[6]).简称()}{this.壬式.取乘将(天盘[7]).简称()}" +
                $"{this.壬式.取乘将(天盘[8]).简称()}{this.壬式.取乘将(天盘[9]).简称()}")
                .AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{天盘[6]:C}{天盘[7]:C}" +
                $"{天盘[8]:C}{天盘[9]:C}")
                .AppendLine(
                $"{空格}{空格}{空格}" +
                $"{this.壬式.取乘将(天盘[5]).简称()}" +
                $"{天盘[5]:C}" +
                $"{空格}{空格}" +
                $"{天盘[10]:C}" +
                $"{this.壬式.取乘将(天盘[10]).简称()}")
                .AppendLine(
                $"{空格}{空格}{空格}" +
                $"{this.壬式.取乘将(天盘[4]).简称()}" +
                $"{天盘[4]:C}" +
                $"{空格}{空格}" +
                $"{天盘[11]:C}" +
                $"{this.壬式.取乘将(天盘[11]).简称()}")
                .AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{天盘[3]:C}{天盘[2]:C}" +
                $"{天盘[1]:C}{天盘[12]:C}")
                .AppendLine(
                $"{空格}{空格}{空格}{空格}" +
                $"{this.壬式.取乘将(天盘[3]).简称()}{this.壬式.取乘将(天盘[2]).简称()}" +
                $"{this.壬式.取乘将(天盘[1]).简称()}{this.壬式.取乘将(天盘[12]).简称()}")
                .AppendLine();
        }

        private void 追加课体(StringBuilder builder)
        {
            var t = this.壬式.课体.Select(体 => 体.课体名).ToArray();
            if (t.Length > 0)
            {
                _ = builder
                    .AppendLine(string.Join(空格, t))
                    .AppendLine();
            }
        }

        private void 追加神煞(StringBuilder builder)
        {
            bool 有内容 = false;
            foreach (var 神煞 in this.壬式.神煞)
            {
                if (神煞.所在神.Count is 0)
                    continue;

                有内容 = true;
                var 内容 = 神煞.所在神.Select(神 => 神.ToString("C"));
                _ = builder.AppendLine($"{神煞.神煞名}：{string.Join(空格, 内容)}");
            }
            if (有内容)
                _ = builder.AppendLine();
        }

        private void 追加参考(StringBuilder builder)
        {
            foreach (var 参考 in this.壬式.占断参考)
            {
                if (参考.内容 is null)
                    continue;

                _ = builder
                    .AppendLine($"{参考.题目}：")
                    .AppendLine(参考.内容)
                    .AppendLine();
            }
        }

        private void 追加断语(StringBuilder builder)
        {
            _ = builder
                .AppendLine(this.断语)
                .AppendLine();
        }

        private void 追加分割线(StringBuilder builder, string 内容)
        {
            _ = builder.AppendLine($"==={内容}============").AppendLine();
        }
        public string 可读文本化()
        {
            StringBuilder builder = new StringBuilder();
            this.追加分割线(builder, "占时");
            this.追加年月日时(builder);
            this.追加年命(builder);
            this.追加分割线(builder, "盘面");
            this.追加三传(builder);
            this.追加四课(builder);
            this.追加天盘(builder);
            this.追加课体(builder);
            this.追加分割线(builder, "断语");
            this.追加断语(builder);
            this.追加分割线(builder, "神煞");
            this.追加神煞(builder);
            this.追加分割线(builder, "参考");
            this.追加参考(builder);
            return builder.ToString();
        }
    }
}
