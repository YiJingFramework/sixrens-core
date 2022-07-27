using SixRens.Core.壬式生成;
using System.Text;

namespace 测试用交互
{
    public partial class 占断参考内容窗体 : Form
    {
        public 占断参考内容窗体(占断参考 参考)
        {
            this.InitializeComponent();
            StringBuilder stringBuilder = new StringBuilder();
            _ = stringBuilder.AppendLine($"{参考.题目}：");
            _ = stringBuilder.AppendLine($"{参考.内容 ?? "没有内容提供"}");
            this.textBox1.Text = stringBuilder.ToString();
        }
    }
}
