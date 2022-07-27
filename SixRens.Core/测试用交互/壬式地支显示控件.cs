using SixRens.Core.壬式生成;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YiJingFramework.StemsAndBranches;

namespace 测试用交互
{
    public partial class 壬式地支显示控件 : UserControl
    {
        public 壬式地支显示控件()
        {
            InitializeComponent();
            this.地支 = new();
        }

        public 壬式? 绑定壬式 { get; set; }

        private EarthlyBranch _地支;
        public EarthlyBranch 地支 
        { 
            get
            {
                return _地支;
            }
            set
            {
                this._地支 = value;
                this.button1.Text = value.ToString("C");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (绑定壬式 is null)
                return;
            new 地支详细信息窗体(绑定壬式, 地支).Show();
        }
    }
}
