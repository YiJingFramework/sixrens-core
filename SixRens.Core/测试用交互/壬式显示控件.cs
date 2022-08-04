using SixRens.Core.壬式生成;
using System.Data;
using YiJingFramework.StemsAndBranches;

namespace 测试用交互
{
    public partial class 壬式显示控件 : UserControl
    {
        public 壬式显示控件()
        {
            this.InitializeComponent();
            this.壬式 = null;
        }

        private void 布置控件(壬式? 壬式)
        {
            if (壬式 is null)
            {
                this.Enabled = false;
                return;
            }
            this.Enabled = true;

            this.年干控件.天干 = 壬式.起课参数.年月日时.年干;
            this.年支控件.地支 = 壬式.起课参数.年月日时.年支;
            this.月干控件.天干 = 壬式.起课参数.年月日时.月干;
            this.月支控件.地支 = 壬式.起课参数.年月日时.月支;
            this.日干控件.天干 = 壬式.起课参数.年月日时.日干;
            this.日支控件.地支 = 壬式.起课参数.年月日时.日支;
            this.时干控件.天干 = 壬式.起课参数.年月日时.时干;
            this.时支控件.地支 = 壬式.起课参数.年月日时.时支;
            this.月将控件.地支 = 壬式.起课参数.年月日时.月将;

            this.初传控件.地支 = 壬式.三传.初传;
            this.中传控件.地支 = 壬式.三传.中传;
            this.末传控件.地支 = 壬式.三传.末传;

            this.初传遁干控件.天干 = 壬式.起课参数.年月日时.旬所在.获取对应天干(壬式.三传.初传);
            this.中传遁干控件.天干 = 壬式.起课参数.年月日时.旬所在.获取对应天干(壬式.三传.中传);
            this.末传遁干控件.天干 = 壬式.起课参数.年月日时.旬所在.获取对应天干(壬式.三传.末传);

            this.初传天将控件.天将 = 壬式.天将盘.取乘将(壬式.三传.初传);
            this.中传天将控件.天将 = 壬式.天将盘.取乘将(壬式.三传.中传);
            this.末传天将控件.天将 = 壬式.天将盘.取乘将(壬式.三传.末传);

            this.日控件.天干 = 壬式.四课.日;
            this.日上控件.地支 = 壬式.四课.日阳;
            this.日上乘将控件.天将 = 壬式.天将盘.取乘将(壬式.四课.日阳);
            this.日阳控件.地支 = 壬式.四课.日阳;
            this.日阴控件.地支 = 壬式.四课.日阴;
            this.日阴乘将控件.天将 = 壬式.天将盘.取乘将(壬式.四课.日阴);

            this.辰控件.地支 = 壬式.四课.辰;
            this.辰上控件.地支 = 壬式.四课.辰阳;
            this.辰上乘将控件.天将 = 壬式.天将盘.取乘将(壬式.四课.辰阳);
            this.辰阳控件.地支 = 壬式.四课.辰阳;
            this.辰阴控件.地支 = 壬式.四课.辰阴;
            this.辰阴乘将控件.天将 = 壬式.天将盘.取乘将(壬式.四课.辰阴);

            var 位置天盘控件表 = new[] {
                this.子位控件,
                this.丑位控件,
                this.寅位控件,
                this.卯位控件,
                this.辰位控件,
                this.巳位控件,
                this.午位控件,
                this.未位控件,
                this.申位控件,
                this.酉位控件,
                this.戌位控件,
                this.亥位控件
            };

            var 地盘 = Enumerable.Range(1, 12)
                .Select(w => new EarthlyBranch(w)).ToArray();
            var 天盘 = 地盘
                .Select(z => 壬式.天地盘.取乘神(z)).ToArray();
            for (int i = 0; i < 12; i++)
                位置天盘控件表[i].地支 = 天盘[i];

            var 位置乘将控件表 = new[] {
                this.子位乘将控件,
                this.丑位乘将控件,
                this.寅位乘将控件,
                this.卯位乘将控件,
                this.辰位乘将控件,
                this.巳位乘将控件,
                this.午位乘将控件,
                this.未位乘将控件,
                this.申位乘将控件,
                this.酉位乘将控件,
                this.戌位乘将控件,
                this.亥位乘将控件
            };
            var 天将盘 = 天盘
                .Select(z => 壬式.天将盘.取乘将(z)).ToArray();
            for (int i = 0; i < 12; i++)
                位置乘将控件表[i].天将 = 天将盘[i];

            foreach (var 控件 in this.Controls)
            {
                if (控件 is 壬式地支显示控件 dz)
                {
                    dz.绑定壬式 = 壬式;
                }
            }
        }

        private 壬式? _壬式;
        public 壬式? 壬式
        {
            get
            {
                return this._壬式;
            }
            set
            {
                this._壬式 = value;
                this.布置控件(value);
            }
        }
    }
}
