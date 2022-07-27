using SixRens.Core.名称转换;
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
    public partial class 地支详细信息窗体 : Form
    {
        public 地支详细信息窗体(壬式 壬式, EarthlyBranch 地支)
        {
            InitializeComponent();
            StringBuilder stringBuilder = new StringBuilder();
            _ = stringBuilder.AppendLine($"{地支.地支名()} {地支.天神名()}");
            _ = stringBuilder.AppendLine(
                $"下临{壬式.取临地(地支).地支名()} 上乘{壬式.取上神(地支).天神名()} 乘{壬式.取乘将(地支)}");
            _ = stringBuilder.AppendLine(
                $"{string.Join(" ", 壬式.取神煞(地支).Select(s => s.神煞名))}");
            this.textBox1.Text = stringBuilder.ToString();
        }
    }
}
