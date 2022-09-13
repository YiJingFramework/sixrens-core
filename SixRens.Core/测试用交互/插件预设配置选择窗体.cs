using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;
using System.Data;

namespace 测试用交互
{
    public partial class 插件预设配置选择窗体 : Form
    {
        private readonly 预设管理器 预设管理器;
        private readonly 插件包管理器 插件包管理器;
        public 插件预设配置选择窗体(插件包管理器 插件包管理器, 预设管理器 预设管理器)
        {
            this.预设管理器 = 预设管理器;
            this.插件包管理器 = 插件包管理器;
            this.InitializeComponent();
            this.更新列表();
        }

        private sealed record 列表项(
            预设 预设)
        {
            public override string ToString()
            {
                return this.预设.预设名;
            }
        }

        private void 更新列表()
        {
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(this.预设管理器.预设列表
                .Select(y => new 列表项(y)).ToArray());
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var 新预设 = await this.预设管理器.新增预设(this.textBox1.Text);
            if (新预设 is null)
                _ = MessageBox.Show("重名了！");
            else
                this.更新列表();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (列表项 s in this.listBox1.SelectedItems)
                _ = new 插件预设配置窗体(this.插件包管理器, s.预设).ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            foreach (列表项 s in this.listBox1.SelectedItems)
                await this.预设管理器.删除预设(s.预设);
            this.更新列表();
        }
    }
}
