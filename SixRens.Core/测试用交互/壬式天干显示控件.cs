﻿using YiJingFramework.StemsAndBranches;

namespace 测试用交互
{
    public partial class 壬式天干显示控件 : UserControl
    {
        public 壬式天干显示控件()
        {
            this.InitializeComponent();
            this.天干 = new();
        }

        private HeavenlyStem? _天干;
        public HeavenlyStem? 天干
        {
            get
            {
                return this._天干;
            }
            set
            {
                this._天干 = value;
                this.button1.Text = value?.ToString("C") ?? string.Empty;
            }
        }
    }
}
