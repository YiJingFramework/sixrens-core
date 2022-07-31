namespace SixRens.Core.占例存取.可序列化类型
{
    internal sealed class 可序列化占例
    {
        public 可序列化占例(占例 占例)
        {
            this.壬式 = new(占例.壬式);
            this.断语 = 占例.断语;
            this.西历时间 = 占例.西历时间?.Ticks;
        }
        public 可序列化占例() { }

        public 可序列化壬式? 壬式 { get; init; }
        public long? 西历时间 { get; set; }
        public string? 断语 { get; init; }

        public 占例 转占例()
        {
            return new 占例(
                (this.壬式 ?? new 可序列化壬式()).转壬式(),
                this.断语 ?? string.Empty,
                this.西历时间.HasValue ? new(this.西历时间.Value) : null);
        }
    }
}
