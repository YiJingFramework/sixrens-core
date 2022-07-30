using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixRens.Core.壬式生成
{
    internal static class 壬式识别码生成器
    {
        private readonly static ConcurrentDictionary<Guid, byte> 曾用识别码 = new();

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
