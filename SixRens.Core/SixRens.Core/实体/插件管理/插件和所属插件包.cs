using SixRens.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixRens.Core.实体.插件管理
{
    public sealed record 插件和所属插件包<T插件>(
        T插件 插件,
        插件包 插件包) 
        where T插件 : I插件
    { }
}
