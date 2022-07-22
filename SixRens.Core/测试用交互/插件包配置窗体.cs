using SixRens.Core;
using SixRens.Core.实体.插件管理;
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
    public partial class 插件包配置窗体 : Form
    {
        private readonly 核心 核心;
        public 插件包配置窗体(核心 核心)
        {
            this.核心 = 核心;
            InitializeComponent();
            刷新列表();
        }

        private void 刷新列表()
        {
            this.listView1.Items.Clear();
            this.listView1.Items.AddRange(
                核心.插件包管理器.插件包
                .Select(
                    包 => new ListViewItem($"{包.名称}（{包.版本号}）")
                    {
                        Tag = 包
                    })
                .ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            if (dia.ShowDialog() is DialogResult.OK)
            {
                using var file = dia.OpenFile();
                核心.插件包管理器.从外部加载插件包(file);
                刷新列表();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem si in this.listView1.SelectedItems)
            {
                核心.插件包管理器.移除插件包((插件包)si.Tag);
            }
            刷新列表();
        }
    }
}
