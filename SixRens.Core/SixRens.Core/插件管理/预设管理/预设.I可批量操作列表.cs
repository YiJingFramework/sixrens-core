namespace SixRens.Core.插件管理.预设管理
{
    public sealed partial class 预设
    {
        public interface I可批量操作列表<T> : IList<T>
        {
            void AddMany(IEnumerable<T> values);
            void InsertMany(int index, IEnumerable<T> values);
            void ReplaceAll(IEnumerable<T> values);
        }
    }
}
