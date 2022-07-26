using SixRens.Core.壬式生成;
using SixRens.Core.年月日时;
using SixRens.Core.插件管理;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YiJingFramework.Core;
using YiJingFramework.StemsAndBranches;

namespace 测试用交互
{
    public partial class 起课界面 : Form
    {
        private readonly 插件包管理器 插件包管理器;
        private readonly Dictionary<char, HeavenlyStem> 天干表 =
            HeavenlyStem.BuildStringStemTable("C")
            .ToDictionary((ss) => ss.s[0], (ss) => ss.stem);
        private readonly Dictionary<char, EarthlyBranch> 地支表 =
            EarthlyBranch.BuildStringBranchTable("C")
            .ToDictionary((sb) => sb.s[0], (sb) => sb.branch);

        private readonly BindingList<string> 活时起课绑定;
        private readonly BindingList<string> 月将绑定;
        private readonly BindingList<string> 昼夜绑定;
        private readonly BindingList<本命项目> 本命绑定;
        public 起课界面(插件包管理器 插件包管理器, 预设管理器 预设管理器)
        {
            this.插件包管理器 = 插件包管理器;
            InitializeComponent();

            活时起课绑定 = new();
            foreach (var d in 地支表.Keys.Select(c => c.ToString()).Prepend("正时"))
                活时起课绑定.Add(d);
            this.comboBox1.DataSource = 活时起课绑定;
            this.comboBox1.SelectedIndex = 0;

            月将绑定 = new();
            foreach (var d in 地支表.Keys.Select(c => c.ToString()).Prepend("自动"))
                月将绑定.Add(d);
            this.comboBox2.DataSource = 月将绑定;
            this.comboBox2.SelectedIndex = 0;

            昼夜绑定 = new();
            foreach (var d in new string[] { "自动", "昼占", "夜占" })
                昼夜绑定.Add(d);
            this.comboBox4.DataSource = 昼夜绑定;
            this.comboBox4.SelectedIndex = 0;

            this.comboBox3.Items.AddRange(
                预设管理器.预设列表.Select(y => new 预设选择栏项目(y)).ToArray());
            this.comboBox3.SelectedIndex = 0;

            本命绑定 = new();
            this.dataGridView1.DataSource = 本命绑定;

            this.button1.Click += (_, _) => this.设置时间(textBox1.Text);

            this.设置时间(DateTime.Now.ToString("Xyyyy/MM/dd/HH/mm"));
        }

        private sealed record 本命项目(本命项目.性别项 性别, 本命项目.本命项 本命, 本命项目.课主项 课主)
        {
            public enum 课主项
            {
                课主, 非课主
            }
            public enum 性别项
            {
                男, 女
            }
            public enum 本命项
            {
                子, 丑, 寅, 卯, 辰, 巳, 午, 未, 申, 酉, 戌, 亥
            }
        }
        private sealed record 预设选择栏项目(预设 预设)
        {
            public override string ToString()
            {
                return 预设.预设名;
            }
        }

        private void 根据时间刷新选项(I年月日时信息 信息, bool 可提供月将)
        {
            活时起课绑定[0] = $"正时（{信息.时支:C}）";
            月将绑定[0] = $"自动（{(可提供月将 ? 信息.月将.ToString("C") : "无")}）";
            昼夜绑定[0] = $"自动（{(信息.昼占 ? "昼占" : "夜占")}）";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var 课主 = 本命绑定.Any(i => i.课主 is 本命项目.课主项.课主) ?
                本命项目.课主项.非课主 : 本命项目.课主项.课主;
            本命绑定.Add(new(本命项目.性别项.男, 本命项目.本命项.子, 课主));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HashSet<int> h = new();
            foreach(DataGridViewCell c in this.dataGridView1.SelectedCells)
            {
                _ = h.Add(c.RowIndex);
            }

            int count = 0;
            foreach(var r in h)
            {
                本命绑定.RemoveAt(r - count);
                count++;
            }
        }

        internal 壬式? 所起壬式 { get; private set; }
        private void button2_Click(object sender, EventArgs e)
        {
            var 年月日时 = (I年月日时信息)this.label2.Tag;
            if (this.comboBox1.SelectedIndex is not 0)
                年月日时 = 年月日时.修改时间(地支表[((string)this.comboBox1.SelectedItem)[0]]);
            if (年月日时.检验八字(false))
                if (MessageBox.Show("指定的年月日时不可能实际存在，要继续吗？", "",
                    MessageBoxButtons.YesNo) is DialogResult.No)
                    return;

            if (this.comboBox2.SelectedIndex is not 0)
                年月日时 = 年月日时.修改信息(地支表[((string)this.comboBox2.SelectedItem)[0]]);
            if (this.comboBox4.SelectedIndex is not 0)
                年月日时 = 年月日时.修改信息(null, this.comboBox4.SelectedItem is "昼占");

            本命信息? 课主 = null;
            List<本命信息> 对象 = new();
            foreach (var 本命 in 本命绑定)
            {
                if (本命.课主 is 本命项目.课主项.课主)
                {
                    if (课主 is not null)
                    {
                        MessageBox.Show("只能有一个课主");
                        return;
                    }
                    课主 = new 本命信息(
                        本命.性别 is 本命项目.性别项.男 ? YinYang.Yang : YinYang.Yin,
                        地支表[本命.本命.ToString()[0]]);
                }
                else
                {
                    对象.Add(new 本命信息(
                        本命.性别 is 本命项目.性别项.男 ? YinYang.Yang : YinYang.Yin,
                        地支表[本命.本命.ToString()[0]]));
                }
            }

            var 预设 = 插件包管理器.解析预设(
                ((预设选择栏项目)this.comboBox3.SelectedItem).预设);
            if (预设 is null)
            {
                _ = MessageBox.Show("不可用的预设。请确认预设中的插件都已安装。");
                return;
            }
            if (预设.存在同时指定了启用和禁用的神煞)
                _ = MessageBox.Show("存在同时指定了启用和禁用的神煞，这些神煞会被视为禁用。");
            if (预设.存在指定启用但未找到的神煞)
                _ = MessageBox.Show("存在指定启用但未找到的神煞，这些神煞不会被生成。");
            if (预设.存在未显式指定的神煞)
                _ = MessageBox.Show("存在未显式指定的神煞，这些神煞会视为启用。");
            if (预设.存在同时指定了启用和禁用的课体)
                _ = MessageBox.Show("存在同时指定了启用和禁用的课体，这些课体会被视为禁用。");
            if (预设.存在指定启用但未找到的课体)
                _ = MessageBox.Show("存在指定启用但未找到的课体，这些课体不会被生成。");
            if (预设.存在未显式指定的课体)
                _ = MessageBox.Show("存在未显式指定的课体，这些课体会视为启用。");

            this.所起壬式 = new 壬式(年月日时, 课主, 对象, 预设);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void 设置时间(string s)
        {
            void 显示提示()
            {
                _ = MessageBox.Show("无法解析此年月日时。" +
                    "可以参考的格式有“X2022/07/25/14/01”或“N壬寅/丁未/己卯/乙亥”。");
            }
            s = s.ToLowerInvariant();
            if (s.StartsWith("x"))
            {
                if(!DateTime.TryParseExact(s, "xyyyy/MM/dd/HH/mm",
                    null, System.Globalization.DateTimeStyles.None, out var dt))
                {
                    显示提示();
                    return;
                }
                this.label2.Text = dt.ToString("Xyyyy/MM/dd/HH/mm");
                var nyrs = new 真实年月日时(dt);
                this.label2.Tag = nyrs;
                根据时间刷新选项(nyrs, true);
                return;
            }
            else if (s.StartsWith("n"))
            {
                var sp = s[1..].Split("/");
                if (sp.Length is not 4 ||
                    sp[0].Length is not 2 ||
                    sp[1].Length is not 2 ||
                    sp[2].Length is not 2 ||
                    sp[3].Length is not 2)
                {
                    显示提示();
                    return;
                }
                if (天干表.TryGetValue(sp[0][0], out var 年干) &&
                    天干表.TryGetValue(sp[1][0], out var 月干) &&
                    天干表.TryGetValue(sp[2][0], out var 日干) &&
                    天干表.TryGetValue(sp[3][0], out var 时干) &&
                    地支表.TryGetValue(sp[0][1], out var 年支) &&
                    地支表.TryGetValue(sp[1][1], out var 月支) &&
                    地支表.TryGetValue(sp[2][1], out var 日支) &&
                    地支表.TryGetValue(sp[3][1], out var 时支))
                {
                    this.label2.Text = $"N{年干:C}{年支:C}/{月干:C}{月支:C}/" +
                        $"{日干:C}{日支:C}/{时干:C}{时支:C}";
                    var nyrs = new 自定义年月日时(
                        年干, 年支,
                        月干, 月支,
                        日干, 日支,
                        时干, 时支,
                        时支.Index is >= 4 and < 10,
                        new EarthlyBranch(1));
                    this.label2.Tag = nyrs;
                    根据时间刷新选项(nyrs, false);
                    return;
                }
                else
                {
                    显示提示();
                    return;
                }
            }
            else
            {
                显示提示();
                return;
            }
        }
    }
}
