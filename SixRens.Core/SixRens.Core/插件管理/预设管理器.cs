using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace SixRens.Core.插件管理
{
    public sealed class 预设管理器
    {
        private readonly DirectoryInfo _文件夹;
        private readonly List<预设> _预设列表;
        private const string 文件名前缀 = "preset_";

        public IReadOnlyList<预设> 预设列表 { get; }

        private string 获取预设文件路径(string 预设名)
        {
            var name = $"{文件名前缀}{预设名}";
            var path = Path.GetFullPath(name, this._文件夹.FullName);
            return path;
        }

        public 预设管理器(DirectoryInfo 预设文件夹)
        {
            this._文件夹 = 预设文件夹;
            this._文件夹.Create();
            this._预设列表 = new();
            this.预设列表 = new ReadOnlyCollection<预设>(this._预设列表);

            foreach (var file in
                预设文件夹.EnumerateFiles().Where(f => f.Name.StartsWith(文件名前缀)))
            {
                var s = File.ReadAllText(file.FullName);
                var 可序列化 = JsonSerializer.Deserialize<预设.可序列化信息>(s);
                var 预设 = new 预设(file.Name[文件名前缀.Length..], 可序列化);
                预设.预设被修改 += (sender, _) => {
                    Debug.Assert(sender is 预设);
                    this.更新预设文件((预设)sender);
                };
                _预设列表.Add(预设);
            }
        }

        public 预设? 新增预设(string 预设名)
        {
            if (_预设列表.Any(y => y.预设名 == 预设名))
                return null;

            var result = new 预设(预设名);
            result.预设被修改 += (sender, _) => {
                Debug.Assert(sender is 预设);
                this.更新预设文件((预设)sender);
            };
            this.更新预设文件(result);
            _预设列表.Add(result);
            return result;
        }

        public void 删除预设(预设 预设)
        {
            if (_预设列表.Remove(预设))
                File.Delete(this.获取预设文件路径(预设.预设名));
        }

        private void 更新预设文件(预设 预设)
        {
            var path = this.获取预设文件路径(预设.预设名);
            var 序列化信息 = 预设.生成序列化信息();
            var s = JsonSerializer.Serialize(序列化信息);
            File.WriteAllText(path, s);
        }
    }
}
