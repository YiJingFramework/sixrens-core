using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixRens.Core.插件管理
{
    public sealed partial class 预设
    {
        public interface I可批量操作列表<T> : IList<T>
        {
            void AddMany(IEnumerable<T> values);
            void InsertMany(int index, IEnumerable<T> values);
            void ReplaceAll(IEnumerable<T> values);
        }
    }
}
