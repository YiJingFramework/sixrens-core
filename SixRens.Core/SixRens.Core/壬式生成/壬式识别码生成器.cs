using System.Collections.Concurrent;

namespace SixRens.Core.壬式生成
{
    internal static class 壬式识别码生成器
    {
        private static readonly ConcurrentDictionary<Guid, byte> 曾用识别码 = new();

        public static Guid 新识别码
        {
            get
            {
                for (; ; )
                {
                    var 识别码 = Guid.NewGuid();
                    if (曾用识别码.TryAdd(识别码, default))
                        return 识别码;
                }
            }
        }
    }
}
