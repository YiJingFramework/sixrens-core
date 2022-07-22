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
    public partial class 主窗体 : Form
    {
        private readonly 核心 核心;
        public 主窗体(核心 核心)
        {
            this.核心 = 核心;
            InitializeComponent();
        }

        private void 插件包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = new 插件包配置窗体(核心).ShowDialog();
        }

        private void 插件预设ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = new 插件预设配置窗体(核心).ShowDialog();
        }
    }
}
