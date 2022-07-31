using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;
using System.ComponentModel;
using System.Data;

namespace 测试用交互
{
    public partial class 编辑神煞或课体启禁用界面 : Form
    {
        private readonly 插件包管理器 插件包管理器;
        private readonly 预设 预设;
        private readonly bool 模式为神煞或课体;
        private DataGridView? 选择项所在;
        private readonly BindingList<列表项> 绑定列表启用;
        private readonly BindingList<列表项> 绑定列表禁用;
        public 编辑神煞或课体启禁用界面(插件包管理器 插件包管理器, 预设 预设, bool 模式为神煞或课体)
        {
            this.插件包管理器 = 插件包管理器;
            this.预设 = 预设;
            this.模式为神煞或课体 = 模式为神煞或课体;

            this.InitializeComponent();
            this.绑定列表启用 = new();
            this.绑定列表禁用 = new();
            this.dataGridView1.DataSource = this.绑定列表启用;
            this.dataGridView2.DataSource = this.绑定列表禁用;

            if (模式为神煞或课体)
            {
                foreach (var 配置 in 预设.神煞启用)
                    this.绑定列表启用.Add(new(配置.题目, 配置.插件));
                foreach (var 配置 in 预设.神煞禁用)
                    this.绑定列表禁用.Add(new(配置.题目, 配置.插件));
            }
            else
            {
                foreach (var 配置 in 预设.课体启用)
                    this.绑定列表启用.Add(new(配置.题目, 配置.插件));
                foreach (var 配置 in 预设.课体禁用)
                    this.绑定列表禁用.Add(new(配置.题目, 配置.插件));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.模式为神煞或课体)
            {
                HashSet<Guid> 欲找 = new HashSet<Guid>(this.预设.神煞插件);
                foreach (var 插件 in this.插件包管理器.神煞插件.Values)
                {
                    var 识别码 = 插件.插件.插件识别码;
                    if (欲找.Remove(识别码))
                    {
                        foreach (var 项目 in 插件.插件.支持的神煞)
                        {
                            if (this.绑定列表启用.Any(i => i.插件 == 识别码 && i.名称 == 项目.神煞名))
                                continue;
                            if (this.绑定列表禁用.Any(i => i.插件 == 识别码 && i.名称 == 项目.神煞名))
                                continue;
                            this.绑定列表启用.Add(new(项目.神煞名, 识别码));
                        }
                    }
                }
            }
            else
            {
                HashSet<Guid> 欲找 = new HashSet<Guid>(this.预设.课体插件);
                foreach (var 插件 in this.插件包管理器.课体插件.Values)
                {
                    var 识别码 = 插件.插件.插件识别码;
                    if (欲找.Remove(识别码))
                    {
                        foreach (var 项目 in 插件.插件.支持的课体)
                        {
                            if (this.绑定列表启用.Any(i => i.插件 == 识别码 && i.名称 == 项目.课体名))
                                continue;
                            if (this.绑定列表禁用.Any(i => i.插件 == 识别码 && i.名称 == 项目.课体名))
                                continue;
                            this.绑定列表启用.Add(new(项目.课体名, 识别码));
                        }
                    }
                }
            }
        }

        private void 设置选择项(dynamic sender, DataGridViewCellEventArgs e)
        {
            this.选择项所在 = sender;
            if (this.选择项所在 == this.dataGridView1)
            {
                this.label1.Text = "启用！";
                this.label2.Text = "禁用";
            }
            else
            {
                this.label1.Text = "启用";
                this.label2.Text = "禁用！";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.模式为神煞或课体)
            {
                for (int i = 0; i < this.绑定列表启用.Count;)
                {
                    if (this.预设.神煞插件.Contains(this.绑定列表启用[i].插件))
                        i++;
                    else
                        this.绑定列表启用.RemoveAt(i);
                }

                for (int i = 0; i < this.绑定列表禁用.Count;)
                {
                    if (this.预设.神煞插件.Contains(this.绑定列表禁用[i].插件))
                        i++;
                    else
                        this.绑定列表禁用.RemoveAt(i);
                }
            }
            else
            {
                for (int i = 0; i < this.绑定列表启用.Count;)
                {
                    if (this.预设.课体插件.Contains(this.绑定列表启用[i].插件))
                        i++;
                    else
                        this.绑定列表启用.RemoveAt(i);
                }

                for (int i = 0; i < this.绑定列表禁用.Count;)
                {
                    if (this.预设.课体插件.Contains(this.绑定列表禁用[i].插件))
                        i++;
                    else
                        this.绑定列表禁用.RemoveAt(i);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.模式为神煞或课体)
            {
                this.预设.神煞启用.ReplaceAll(this.绑定列表启用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
                this.预设.神煞禁用.ReplaceAll(this.绑定列表禁用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
            }
            else
            {
                this.预设.课体启用.ReplaceAll(this.绑定列表启用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
                this.预设.课体禁用.ReplaceAll(this.绑定列表禁用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var id = new Guid(this.textBox1.Text);
            var name = this.textBox1.Name;
            if (this.选择项所在 is null)
            {
                this.绑定列表启用.Add(new(name, id));
            }
            else
            {
                var lst = (BindingList<列表项>)this.选择项所在.DataSource;
                lst.Insert(this.选择项所在.SelectedCells[0].RowIndex + 1, new(name, id));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.选择项所在 is not null)
            {
                var lst = (BindingList<列表项>)this.选择项所在.DataSource;
                lst.RemoveAt(this.选择项所在.SelectedCells[0].RowIndex);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.选择项所在 is not null)
            {
                if (this.选择项所在 == this.dataGridView1)
                {
                    HashSet<int> rows = new();
                    foreach (DataGridViewCell c in this.dataGridView1.SelectedCells)
                    {
                        _ = rows.Add(c.RowIndex);
                    }

                    int count = 0;
                    foreach (var r in rows)
                    {
                        var item = this.绑定列表启用[r - count];
                        this.绑定列表启用.RemoveAt(r - count);
                        this.绑定列表禁用.Add(item);
                        count++;
                    }
                }
                else
                {
                    HashSet<int> rows = new();
                    foreach (DataGridViewCell c in this.dataGridView2.SelectedCells)
                    {
                        _ = rows.Add(c.RowIndex);
                    }

                    int count = 0;
                    foreach (var r in rows)
                    {
                        var item = this.绑定列表禁用[r - count];
                        this.绑定列表禁用.RemoveAt(r - count);
                        this.绑定列表启用.Add(item);
                        count++;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.选择项所在 is not null)
            {
                HashSet<int> rows = new();
                foreach (DataGridViewCell c in this.选择项所在.SelectedCells)
                {
                    _ = rows.Add(c.RowIndex);
                }

                var lst = (BindingList<列表项>)this.选择项所在.DataSource;
                for (int i = 0; ; i++)
                {
                    if (!rows.Remove(i))
                        break;
                }
                foreach (var r in rows.OrderBy(i => i))
                {
                    var item = lst[r];
                    lst.RemoveAt(r);
                    lst.Insert(r - 1, item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.选择项所在 is not null)
            {
                HashSet<int> rows = new();
                foreach (DataGridViewCell c in this.选择项所在.SelectedCells)
                {
                    _ = rows.Add(c.RowIndex);
                }

                var lst = (BindingList<列表项>)this.选择项所在.DataSource;
                for (int i = lst.Count - 1; ; i--)
                {
                    if (!rows.Remove(i))
                        break;
                }
                foreach (var r in rows.OrderByDescending(i => i))
                {
                    var item = lst[r];
                    lst.RemoveAt(r);
                    lst.Insert(r + 1, item);
                }
            }
        }

        private sealed record 列表项(string 名称, Guid 插件) { }
    }
}
