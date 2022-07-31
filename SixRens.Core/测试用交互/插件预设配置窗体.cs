using SixRens.Api;
using SixRens.Core.插件管理.插件包管理;
using SixRens.Core.插件管理.预设管理;
using System.Diagnostics;

namespace 测试用交互
{
    public partial class 插件预设配置窗体 : Form
    {
        private readonly 插件包管理器 插件包管理器;
        private readonly 预设 预设;

        private sealed record 列表项<T>(
            string 字符串表达,
            T 内容)
        {
            public override string ToString()
            {
                return this.字符串表达;
            }
        }

        private void 初始化ListView<T>(
            ListView listview,
            IEnumerable<插件和所属插件包<T>> 插件表,
            Guid? 目前值, Action<Guid?> 修改行为)
            where T : I插件
        {
            bool 包含目前值 = false;
            foreach (var 插件 in 插件表)
            {
                var item = new ListViewItem(插件.插件.插件名 ?? 插件.插件.插件识别码.ToString()) {
                    Tag = 插件
                };
                _ = listview.Items.Add(item);
                if (插件.插件.插件识别码 == 目前值)
                {
                    item.Selected = true;
                    包含目前值 = true;
                }
            }
            if (!包含目前值 && 目前值.HasValue)
            {
                _ = listview.Items.Add(
                    new ListViewItem(目前值.ToString()) {
                        Tag = 目前值.Value,
                        Selected = true
                    });
            }
            listview.Tag = (ListView.SelectedListViewItemCollection c) => {
                if (c.Count == 0)
                {
                    修改行为(null);
                    return;
                }
                var c0 = c[0].Tag;
                if (c0 is Guid id)
                    修改行为(id);
                else
                {
                    Debug.Assert(c0 is 插件和所属插件包<T>);
                    修改行为(((插件和所属插件包<T>)c0).插件.插件识别码);
                }
            };
            listview.SelectedIndexChanged += this.选项改变;
        }

        private void 初始化ListView<T>(
            ListView listview,
            IEnumerable<插件和所属插件包<T>> 插件表,
            IList<Guid> 预设表)
            where T : I插件
        {
            var 还想要的值 = new List<Guid>(预设表);
            foreach (var 插件 in 插件表)
            {
                var item = new ListViewItem(插件.插件.插件名 ?? 插件.插件.插件识别码.ToString()) {
                    Tag = 插件
                };
                _ = listview.Items.Add(item);
                if (还想要的值.Remove(插件.插件.插件识别码))
                {
                    item.Selected = true;
                }
            }
            foreach (var 值 in 还想要的值)
            {
                _ = listview.Items.Add(
                    new ListViewItem(值.ToString()) {
                        Tag = 值,
                        Selected = true
                    });
            }

            listview.Tag = (ListView.SelectedListViewItemCollection c) => {
                预设表.Clear();
                foreach (ListViewItem i in c)
                {
                    if (i.Tag is Guid id)
                        预设表.Add(id);
                    else
                    {
                        Debug.Assert(i.Tag is 插件和所属插件包<T>);
                        预设表.Add(((插件和所属插件包<T>)i.Tag).插件.插件识别码);
                    }
                }
            };
            listview.SelectedIndexChanged += this.选项改变;
        }
        public 插件预设配置窗体(插件包管理器 插件包管理器, 预设 预设)
        {
            this.插件包管理器 = 插件包管理器;
            this.预设 = 预设;
            this.InitializeComponent();

            this.初始化ListView(this.listView1, 插件包管理器.地盘插件.Values, 预设.地盘插件, id => 预设.地盘插件 = id);
            this.初始化ListView(this.listView2, 插件包管理器.天盘插件.Values, 预设.天盘插件, id => 预设.天盘插件 = id);
            this.初始化ListView(this.listView3, 插件包管理器.四课插件.Values, 预设.四课插件, id => 预设.四课插件 = id);
            this.初始化ListView(this.listView4, 插件包管理器.三传插件.Values, 预设.三传插件, id => 预设.三传插件 = id);
            this.初始化ListView(this.listView5, 插件包管理器.天将插件.Values, 预设.天将插件, id => 预设.天将插件 = id);
            this.初始化ListView(this.listView6, 插件包管理器.年命插件.Values, 预设.年命插件, id => 预设.年命插件 = id);

            this.初始化ListView(this.listView7, 插件包管理器.神煞插件.Values, 预设.神煞插件);
            this.初始化ListView(this.listView8, 插件包管理器.课体插件.Values, 预设.课体插件);
            this.初始化ListView(this.listView9, 插件包管理器.参考插件.Values, 预设.参考插件);

            this.button3.Click += (_, _) => this.另外添加(this.listView1);
            this.button4.Click += (_, _) => this.另外添加(this.listView2);
            this.button5.Click += (_, _) => this.另外添加(this.listView3);
            this.button6.Click += (_, _) => this.另外添加(this.listView4);
            this.button7.Click += (_, _) => this.另外添加(this.listView5);
            this.button8.Click += (_, _) => this.另外添加(this.listView6);
            this.button9.Click += (_, _) => this.另外添加(this.listView7);
            this.button10.Click += (_, _) => this.另外添加(this.listView8);
            this.button11.Click += (_, _) => this.另外添加(this.listView9);
        }

        private void 另外添加(ListView listView)
        {
            Guid id = new Guid(this.textBox1.Text);
            foreach (ListViewItem item in listView.Items)
            {
                if (item.Tag is Guid itid)
                {
                    if (id == itid)
                        return;
                }
                dynamic p = item.Tag;
                itid = p.插件.插件识别码;
                if (itid == id)
                    return;
            }
            _ = listView.Items.Add(
                new ListViewItem(id.ToString()) {
                    Tag = id
                });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new 编辑神煞或课体启禁用界面(this.插件包管理器, this.预设, true).ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new 编辑神煞或课体启禁用界面(this.插件包管理器, this.预设, false).ShowDialog();
        }

        private void 选项改变(object? sender, EventArgs e)
        {
            Debug.Assert(sender is ListView);
            var lv = (ListView)sender;
            dynamic tag = lv.Tag;
            tag(lv.SelectedItems);
        }
    }
}
