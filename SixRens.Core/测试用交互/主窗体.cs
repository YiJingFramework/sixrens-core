using SixRens.Core.占例存取;
using SixRens.Core.壬式生成;
using SixRens.Core.插件管理;
using System.Diagnostics;

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

        private 占例? 占例;

        private void 起课ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var 起课 = new 起课界面(this.插件包管理器, this.预设管理器);
            if (起课.ShowDialog() is DialogResult.OK)
            {
                var 壬式 = 起课.所起壬式;
                Debug.Assert(壬式 is not null);
                var 占例 = 壬式.创建占例();
                占例.断语 = $"{占例.可读文本化()}";
                this.加载占例(占例);
            }
        }

        private sealed record 占断参考选项(
            占断参考 参考)
        {
            public override string ToString()
            {
                return $"{this.参考.题目}";
            }
        }

        private void 加载占例(占例 占例)
        {
            this.占例 = 占例;
            this.壬式显示控件1.壬式 = 占例.壬式;
            this.textBox2.Text = 占例.断语;
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(
                占例.壬式.占断参考.Select(ck => new 占断参考选项(ck)).ToArray());
        }

        private void 打开占例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() is DialogResult.OK)
                this.加载占例(占例.反序列化(File.ReadAllText(dialog.FileName)));
        }

        private void 保存占例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.占例 is null)
                return;
            this.占例.断语 = this.textBox2.Text;
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() is DialogResult.OK)
                File.WriteAllText(dialog.FileName, this.占例.序列化());
        }

        private void 导出占例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.占例 is null)
                return;

            var r = MessageBox.Show("直接保存右侧断语？", "", MessageBoxButtons.YesNoCancel);
            if (r is DialogResult.Cancel)
                return;

            this.占例.断语 = this.textBox2.Text;
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() is DialogResult.OK)
                File.WriteAllText(dialog.FileName,
                    r is DialogResult.Yes ? this.textBox2.Text : this.占例.可读文本化());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem is 占断参考选项 xx) // not null
            {
                new 占断参考内容窗体(xx.参考).Show();
            }
        }
    }
}
