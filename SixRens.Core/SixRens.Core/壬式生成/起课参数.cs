using SixRens.Api.实体.起课信息;
using SixRens.Core.年月日时;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixRens.Core.壬式生成
{
    public sealed class 起课参数 : I起课信息
    {
        internal 起课参数(I年月日时信息 年月日时, 年命? 课主年命, IEnumerable<年命> 对象年命)
        {
            this.年月日时 = 年月日时.生成年月日时();
            this.课主年命 = 课主年命;
            this.对象年命 = Array.AsReadOnly(对象年命.ToArray());
        }
        public 起课参数(I年月日时信息 年月日时, I年命? 课主年命, IEnumerable<I年命> 对象年命)
        {
            this.年月日时 = 年月日时.生成年月日时();
            this.课主年命 = 课主年命 is null ? null : new 年命(课主年命);
            this.对象年命 = Array.AsReadOnly(对象年命.Select(年命 => new 年命(年命)).ToArray());
        }
        public I年月日时 年月日时 { get; }
        public 年命? 课主年命 { get; }
        I年命? I起课信息.课主年命 => this.课主年命;
        public IReadOnlyList<年命> 对象年命 { get; }
        IReadOnlyList<I年命> I起课信息.对象年命 => this.对象年命;
    }
}
