using SixRens.Api.实体;
using SixRens.Core.名称转换;

namespace 测试用交互
{
    public partial class 壬式天将显示控件 : UserControl
    {
        public 壬式天将显示控件()
        {
            this.InitializeComponent();
            this.天将 = new();
        }

        private 天将 _天将;
        public 天将 天将
        {
            get
            {
                return this._天将;
            }
            set
            {
                this._天将 = value;
                this.button1.Text = value.简称();
            }
        }
    }
}
