using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace SixRens.Core.插件管理.预设管理
{
    public sealed class 预设管理器
    {
        private readonly I预设管理器储存器 _储存器;
        private readonly List<预设> _预设列表;
        private readonly EventHandler 事件绑定;
        public IReadOnlyList<预设> 预设列表 { get; }
        private void 绑定事件并添加到列表(预设 预设)
        {
            预设.预设被修改 += 事件绑定;
            this._预设列表.Add(预设);
        }

        private void 更新预设文件(预设 预设)
        {
            var s = 预设.序列化();
            this._储存器.储存预设文件(预设.预设名, s);
        }

        public 预设管理器(I预设管理器储存器 预设管理器储存器)
        {
            this.事件绑定 = new EventHandler((sender, _) => {
                Debug.Assert(sender is 预设);
                this.更新预设文件((预设)sender);
            });

            this._储存器 = 预设管理器储存器;
            this._预设列表 = new();
            this.预设列表 = new ReadOnlyCollection<预设>(this._预设列表);

            foreach (var (预设名, 预设内容) in this._储存器.获取所有预设文件())
            {
                var 设 = 预设.反序列化(预设名, 预设内容);
                绑定事件并添加到列表(设);
            }
        }

        public 预设? 新增预设(string 预设名)
        {
            if (this._预设列表.Any(y => y.预设名 == 预设名))
                return null;
            if (!this._储存器.新建预设文件(预设名))
                return null;

            var result = new 预设(预设名);
            this.更新预设文件(result);
            this.绑定事件并添加到列表(result);
            return result;
        }

        public void 删除预设(预设 预设)
        {
            if (this._预设列表.Remove(预设))
            {
                预设.预设被修改 -= 事件绑定;
                this._储存器.移除预设文件(预设.预设名);
            }
        }

        public 预设? 导入预设文件内容(string 预设名, string 预设文件内容)
        {
            if (this._预设列表.Any(y => y.预设名 == 预设名))
                return null;
            if (!this._储存器.新建预设文件(预设名))
                return null;

            var 设 = 预设.反序列化(预设名, 预设文件内容);
            更新预设文件(设);
            绑定事件并添加到列表(设);
            return 设;
        }

        public string 导出预设文件内容(预设 预设)
        {
            return 预设.序列化();
        }
    }
}
