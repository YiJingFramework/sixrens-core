using SixRens.Core;
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

            InitializeComponent();
            this.绑定列表启用 = new();
            this.绑定列表禁用 = new();
            dataGridView1.DataSource = 绑定列表启用;
            dataGridView2.DataSource = 绑定列表禁用;

            if (模式为神煞或课体)
            {
                foreach (var 配置 in 预设.神煞启用)
                    绑定列表启用.Add(new(配置.题目, 配置.插件));
                foreach (var 配置 in 预设.神煞禁用)
                    绑定列表禁用.Add(new(配置.题目, 配置.插件));
            }
            else
            {
                foreach (var 配置 in 预设.课体启用)
                    绑定列表启用.Add(new(配置.题目, 配置.插件));
                foreach (var 配置 in 预设.课体禁用)
                    绑定列表禁用.Add(new(配置.题目, 配置.插件));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (模式为神煞或课体)
            {
                HashSet<Guid> 欲找 = new HashSet<Guid>(预设.神煞插件);
                foreach (var 插件 in 插件包管理器.神煞插件)
                {
                    var 识别码 = 插件.插件.插件识别码;
                    if (欲找.Remove(识别码))
                    {
                        foreach (var 项目 in 插件.插件.支持的神煞)
                        {
                            if (绑定列表启用.Any(i => i.插件 == 识别码 && i.名称 == 项目.神煞名))
                                continue;
                            if (绑定列表禁用.Any(i => i.插件 == 识别码 && i.名称 == 项目.神煞名))
                                continue;
                            绑定列表启用.Add(new(项目.神煞名, 识别码));
                        }
                    }
                }
            }
            else
            {
                HashSet<Guid> 欲找 = new HashSet<Guid>(预设.课体插件);
                foreach (var 插件 in 插件包管理器.课体插件)
                {
                    var 识别码 = 插件.插件.插件识别码;
                    if (欲找.Remove(识别码))
                    {
                        foreach (var 项目 in 插件.插件.支持的课体)
                        {
                            if (绑定列表启用.Any(i => i.插件 == 识别码 && i.名称 == 项目.课体名))
                                continue;
                            if (绑定列表禁用.Any(i => i.插件 == 识别码 && i.名称 == 项目.课体名))
                                continue;
                            绑定列表启用.Add(new(项目.课体名, 识别码));
                        }
                    }
                }
            }
        }

        private void 设置选择项(dynamic sender, DataGridViewCellEventArgs e)
        {
            选择项所在 = sender;
            if(选择项所在 == dataGridView1)
            {
                label1.Text = "启用！";
                label2.Text = "禁用";
            }
            else
            {
                label1.Text = "启用";
                label2.Text = "禁用！";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (模式为神煞或课体)
            {
                for(int i = 0; i < 绑定列表启用.Count; )
                {
                    if (预设.神煞插件.Contains(绑定列表启用[i].插件))
                        i++;
                    else
                        绑定列表启用.RemoveAt(i);
                }

                for (int i = 0; i < 绑定列表禁用.Count;)
                {
                    if (预设.神煞插件.Contains(绑定列表禁用[i].插件))
                        i++;
                    else
                        绑定列表禁用.RemoveAt(i);
                }
            }
            else
            {
                for (int i = 0; i < 绑定列表启用.Count;)
                {
                    if (预设.课体插件.Contains(绑定列表启用[i].插件))
                        i++;
                    else
                        绑定列表启用.RemoveAt(i);
                }

                for (int i = 0; i < 绑定列表禁用.Count;)
                {
                    if (预设.课体插件.Contains(绑定列表禁用[i].插件))
                        i++;
                    else
                        绑定列表禁用.RemoveAt(i);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (模式为神煞或课体)
            {
                预设.神煞启用.ReplaceAll(this.绑定列表启用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
                预设.神煞禁用.ReplaceAll(this.绑定列表禁用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
            }
            else
            {
                预设.课体启用.ReplaceAll(this.绑定列表启用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
                预设.课体禁用.ReplaceAll(this.绑定列表禁用
                    .Select(i => new 预设.实体题目和所属插件识别码(i.名称, i.插件)));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var id = new Guid(this.textBox1.Text);
            var name = this.textBox1.Name;
            if (选择项所在 is null)
            {
                this.绑定列表启用.Add(new(name, id));
            }
            else
            {
                var lst = (BindingList<列表项>)选择项所在.DataSource;
                lst.Insert(选择项所在.SelectedCells[0].RowIndex + 1, new(name, id));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (选择项所在 is not null)
            {
                var lst = (BindingList<列表项>)选择项所在.DataSource;
                lst.RemoveAt(选择项所在.SelectedCells[0].RowIndex);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (选择项所在 is not null)
            {
                if(选择项所在 == dataGridView1)
                {
                    HashSet<int> rows = new();
                    foreach(DataGridViewCell c in dataGridView1.SelectedCells)
                    {
                        _ = rows.Add(c.RowIndex);
                    }

                    int count = 0;
                    foreach (var r in rows)
                    {
                        var item = 绑定列表启用[r - count];
                        绑定列表启用.RemoveAt(r - count);
                        绑定列表禁用.Add(item);
                        count++;
                    }
                }
                else
                {
                    HashSet<int> rows = new();
                    foreach (DataGridViewCell c in dataGridView2.SelectedCells)
                    {
                        _ = rows.Add(c.RowIndex);
                    }

                    int count = 0;
                    foreach (var r in rows)
                    {
                        var item = 绑定列表禁用[r - count];
                        绑定列表禁用.RemoveAt(r - count);
                        绑定列表启用.Add(item);
                        count++;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (选择项所在 is not null)
            {
                HashSet<int> rows = new();
                foreach (DataGridViewCell c in 选择项所在.SelectedCells)
                {
                    _ = rows.Add(c.RowIndex);
                }

                var lst = (BindingList<列表项>)选择项所在.DataSource;
                for(int i = 0; ;i++)
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
            if (选择项所在 is not null)
            {
                HashSet<int> rows = new();
                foreach (DataGridViewCell c in 选择项所在.SelectedCells)
                {
                    _ = rows.Add(c.RowIndex);
                }

                var lst = (BindingList<列表项>)选择项所在.DataSource;
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
