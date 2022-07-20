namespace SixRens.Core.工具.插件管理
{
    [Serializable]
    public class 插件包读取异常 : Exception
    {
        public 插件包读取异常() { }
        public 插件包读取异常(string message) : base(message) { }
        public 插件包读取异常(string message, Exception inner) : base(message, inner) { }
        protected 插件包读取异常(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
