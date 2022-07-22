using SixRens.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试用交互
{
    public partial class 插件预设配置窗体 : Form
    {
        private readonly 核心 核心;
        public 插件预设配置窗体(核心 核心)
        {
            this.核心 = 核心;
            InitializeComponent();
        }
    }
}
