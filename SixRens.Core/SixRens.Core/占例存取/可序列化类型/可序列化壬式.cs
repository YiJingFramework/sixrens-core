using SixRens.Core.壬式生成;
using SixRens.Core.年月日时;

namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化壬式
    {
        public 可序列化壬式(壬式 壬式)
        {
            this.年月日时 = new(壬式.年月日时);
            this.地盘 = new(壬式.地盘);
            this.天盘 = new(壬式.天盘);
            this.四课 = new(壬式.四课);
            this.三传 = new(壬式.三传);
            this.天将盘 = new(壬式.天将盘);
            this.课主年命 = 壬式.课主年命 is null ? null : new(壬式.课主年命);
            this.对象年命 = 壬式.对象年命.Select(n => new 可序列化年命(n)).ToArray();
            this.神煞 = 壬式.神煞.Select(s => new 可序列化神煞(s)).ToArray();
            this.课体 = 壬式.课体.Select(k => new 可序列化课体(k)).ToArray();
            this.占断参考 = 壬式.占断参考.Select(c => new 可序列化占断参考(c)).ToArray();
        }
        public 可序列化壬式() { }

        public 可序列化年月日时? 年月日时 { get; init; }
        public 可序列化地盘? 地盘 { get; init; }
        public 可序列化天盘? 天盘 { get; init; }
        public 可序列化四课? 四课 { get; init; }
        public 可序列化三传? 三传 { get; init; }
        public 可序列化天将盘? 天将盘 { get; init; }
        public 可序列化年命? 课主年命 { get; init; }
        public 可序列化年命[]? 对象年命 { get; init; }
        public 可序列化神煞[]? 神煞 { get; init; }
        public 可序列化课体[]? 课体 { get; init; }
        public 可序列化课体[]? 启用的课体 { get; init; }
        public 可序列化占断参考[]? 占断参考 { get; init; }

        private static T 非空化<T>(T? t) where T : new()
        {
            return t ?? new T();
        }
        private static T[] 非空化<T>(T[]? ts)
        {
            return ts ?? Array.Empty<T>();
        }

        public 壬式 转壬式()
        {
            return new 壬式(
                非空化(this.年月日时).转年月日时().生成年月日时(),
                非空化(this.地盘).转地盘(),
                非空化(this.天盘).转天盘(),
                非空化(this.四课).转四课(),
                非空化(this.三传).转三传(),
                非空化(this.天将盘).转天将盘(),
                this.课主年命?.转年命(),
                非空化(this.对象年命).Select(n => n.转年命()),
                非空化(this.神煞).Select(n => n.转神煞()),
                非空化(this.课体).Select(n => n.转课体()),
                非空化(this.启用的课体).Select(n => n.转课体()),
                非空化(this.占断参考).Select(n => n.转占断参考()));
        }
    }
}