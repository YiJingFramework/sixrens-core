using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace SixRens.Core.插件管理.预设管理
{
    public sealed class 预设管理器
    {
        private readonly I预设管理器储存器 _储存器;
        private readonly List<预设> _预设列表;

        public IReadOnlyList<预设> 预设列表 { get; }

        public 预设管理器(I预设管理器储存器 预设管理器储存器)
        {
            this._储存器 = 预设管理器储存器;
            this._预设列表 = new();
            this.预设列表 = new ReadOnlyCollection<预设>(this._预设列表);

            foreach (var (预设名, 预设内容) in this._储存器.获取所有预设文件())
            {
                var 可序列化 = JsonSerializer.Deserialize<预设.可序列化信息>(预设内容);
                var 预设 = new 预设(预设名, 可序列化);
                预设.预设被修改 += (sender, _) => {
                    Debug.Assert(sender is 预设);
                    this.更新预设文件((预设)sender);
                };
                this._预设列表.Add(预设);
            }
        }

        public 预设? 新增预设(string 预设名)
        {
            if (this._预设列表.Any(y => y.预设名 == 预设名))
                return null;
            if (!this._储存器.新建预设文件(预设名))
                return null;

            var result = new 预设(预设名);
            result.预设被修改 += (sender, _) => {
                Debug.Assert(sender is 预设);
                this.更新预设文件((预设)sender);
            };
            this.更新预设文件(result);
            this._预设列表.Add(result);
            return result;
        }

        public void 删除预设(预设 预设)
        {
            if (this._预设列表.Remove(预设))
                this._储存器.移除预设文件(预设.预设名);
        }

        private void 更新预设文件(预设 预设)
        {
            var 序列化信息 = 预设.生成序列化信息();
            var s = JsonSerializer.Serialize(序列化信息);
            this._储存器.储存预设文件(预设.预设名, s);
        }
    }
}
