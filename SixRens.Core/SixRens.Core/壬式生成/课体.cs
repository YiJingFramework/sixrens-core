﻿using SixRens.Api.实体;

namespace SixRens.Core.壬式生成
{
    public sealed class 课体 : I课体
    {
        internal 课体(Guid 所用插件, string 课体)
        {
            this.所用插件 = 所用插件;
            this.课体名 = 课体;
        }
        public Guid 所用插件 { get; }
        public string 课体名 { get; }
    }
}
