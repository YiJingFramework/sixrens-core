namespace SixRens.Core.插件管理.预设管理
{
    public sealed partial class 预设
    {
        public event EventHandler? 预设被修改;

        private Guid? _地盘插件;
        public Guid? 地盘插件
        {
            get { return this._地盘插件; }
            set
            {
                this._地盘插件 = value;
                预设被修改?.Invoke(this, EventArgs.Empty);
            }
        }

        private Guid? _天盘插件;
        public Guid? 天盘插件
        {
            get { return this._天盘插件; }
            set
            {
                this._天盘插件 = value;
                预设被修改?.Invoke(this, EventArgs.Empty);
            }
        }

        private Guid? _四课插件;
        public Guid? 四课插件
        {
            get { return this._四课插件; }
            set
            {
                this._四课插件 = value;
                预设被修改?.Invoke(this, EventArgs.Empty);
            }
        }

        private Guid? _三传插件;
        public Guid? 三传插件
        {
            get { return this._三传插件; }
            set
            {
                this._三传插件 = value;
                预设被修改?.Invoke(this, EventArgs.Empty);
            }
        }

        private Guid? _天将插件;
        public Guid? 天将插件
        {
            get { return this._天将插件; }
            set
            {
                this._天将插件 = value;
                预设被修改?.Invoke(this, EventArgs.Empty);
            }
        }

        private Guid? _年命插件;
        public Guid? 年命插件
        {
            get { return this._年命插件; }
            set
            {
                this._年命插件 = value;
                预设被修改?.Invoke(this, EventArgs.Empty);
            }
        }


        public I可批量操作列表<Guid> 神煞插件 { get; }
        public I可批量操作列表<Guid> 课体插件 { get; }
        public I可批量操作列表<Guid> 参考插件 { get; }

        public I可批量操作列表<实体题目和所属插件识别码> 神煞启用 { get; }
        public I可批量操作列表<实体题目和所属插件识别码> 神煞禁用 { get; }
        public I可批量操作列表<实体题目和所属插件识别码> 课体启用 { get; }
        public I可批量操作列表<实体题目和所属插件识别码> 课体禁用 { get; }

        public string 预设名 { get; }

        internal 预设(string 预设名, 可序列化信息? 信息 = null)
        {
            this.预设名 = 预设名;

            信息 = 信息 ?? new();

            this._地盘插件 = 信息.地盘插件;
            this._天盘插件 = 信息.天盘插件;
            this._四课插件 = 信息.四课插件;
            this._三传插件 = 信息.三传插件;
            this._天将插件 = 信息.天将插件;
            this._年命插件 = 信息.年命插件;

            this.神煞插件 = new 绑定列表<Guid>(this, 信息.神煞插件);
            this.课体插件 = new 绑定列表<Guid>(this, 信息.课体插件);
            this.参考插件 = new 绑定列表<Guid>(this, 信息.参考插件);


            this.神煞启用 = new 绑定列表<实体题目和所属插件识别码>(this, 信息.神煞启用);
            this.神煞禁用 = new 绑定列表<实体题目和所属插件识别码>(this, 信息.神煞禁用);
            this.课体启用 = new 绑定列表<实体题目和所属插件识别码>(this, 信息.课体启用);
            this.课体禁用 = new 绑定列表<实体题目和所属插件识别码>(this, 信息.课体禁用);
        }

        internal 可序列化信息 生成序列化信息()
        {
            return new 可序列化信息() {
                地盘插件 = this.地盘插件,
                天盘插件 = this.天盘插件,
                四课插件 = this.四课插件,
                三传插件 = this.三传插件,
                天将插件 = this.天将插件,
                年命插件 = this.年命插件,
                神煞插件 = new(this.神煞插件),
                课体插件 = new(this.课体插件),
                参考插件 = new(this.参考插件),
                神煞启用 = new(this.神煞启用),
                神煞禁用 = new(this.神煞禁用),
                课体启用 = new(this.课体启用),
                课体禁用 = new(this.课体禁用)
            };
        }
    }
}
