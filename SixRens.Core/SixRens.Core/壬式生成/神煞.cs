﻿using SixRens.Api.实体;
using YiJingFramework.StemsAndBranches;

namespace SixRens.Core.壬式生成
{
    public sealed class 神煞 : I神煞
    {
        internal 神煞(Guid 所用插件, string 神煞题目, I神煞内容 神煞内容)
        {
            this.所用插件 = 所用插件;
            this.神煞名 = 神煞题目;
            this.所在神 = Array.AsReadOnly(神煞内容.所在神.ToArray());
        }
        public Guid 所用插件 { get; }
        public string 神煞名 { get; }
        public IReadOnlyList<EarthlyBranch> 所在神 { get; }
    }
}
