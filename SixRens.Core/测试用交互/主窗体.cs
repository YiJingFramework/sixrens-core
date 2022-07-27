using SixRens.Api.实体;
using SixRens.Core;
using SixRens.Core.占例存取;
using SixRens.Core.壬式生成;
using SixRens.Core.插件管理;
using System.Diagnostics;
using System.Text;

namespace 测试用交互
{
    public partial class 主窗体 : Form
    {
        private readonly 插件包管理器 插件包管理器;
        private readonly 预设管理器 预设管理器;
        public 主窗体(插件包管理器 插件包管理器, 预设管理器 预设管理器)
        {
            this.插件包管理器 = 插件包管理器;
            this.预设管理器 = 预设管理器;
            this.InitializeComponent();
        }

        private void 插件包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = new 插件包配置窗体(this.插件包管理器).ShowDialog();
        }

        private void 插件预设ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = new 插件预设配置选择窗体(this.插件包管理器, this.预设管理器).ShowDialog();
        }

        private void 起课ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var 起课 = new 起课界面(插件包管理器, 预设管理器);
            if (起课.ShowDialog() is DialogResult.OK)
            {
                var 壬式 = 起课.所起壬式;
                Debug.Assert(壬式 is not null);
                var 占例 = 壬式.创建占例();
                this.壬式显示控件1.壬式 = 壬式;
            }
        }
    }
}
