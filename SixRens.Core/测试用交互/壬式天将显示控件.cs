using SixRens.Api.实体;
using SixRens.Core.名称转换;
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
    public partial class 壬式天将显示控件 : UserControl
    {
        public 壬式天将显示控件()
        {
            InitializeComponent();
            this.天将 = new();
        }

        private 天将 _天将;
        public 天将 天将
        { 
            get
            {
                return _天将;
            }
            set
            {
                this._天将 = value;
                this.button1.Text = value.简称();
            }
        }
    }
}
