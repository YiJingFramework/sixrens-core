using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SixRens.Core.工具.名称转换
{
    public sealed class 名称表<T> : IDictionary<T, string> where T : notnull
    {
        private readonly Dictionary<T, string> 字典 = new(12);

        public string this[T key] { get => ((IDictionary<T, string>)this.字典)[key]; set => ((IDictionary<T, string>)this.字典)[key] = value; }

        public ICollection<T> Keys => ((IDictionary<T, string>)this.字典).Keys;

        ICollection<string> IDictionary<T, string>.Values => ((IDictionary<T, string>)this.字典).Values;

        int ICollection<KeyValuePair<T, string>>.Count => ((ICollection<KeyValuePair<T, string>>)this.字典).Count;

        bool ICollection<KeyValuePair<T, string>>.IsReadOnly => ((ICollection<KeyValuePair<T, string>>)this.字典).IsReadOnly;

        public void Add(T key, string value)
        {
            ((IDictionary<T, string>)this.字典).Add(key, value);
        }

        void ICollection<KeyValuePair<T, string>>.Add(KeyValuePair<T, string> item)
        {
            ((ICollection<KeyValuePair<T, string>>)this.字典).Add(item);
        }

        void ICollection<KeyValuePair<T, string>>.Clear()
        {
            ((ICollection<KeyValuePair<T, string>>)this.字典).Clear();
        }

        bool ICollection<KeyValuePair<T, string>>.Contains(KeyValuePair<T, string> item)
        {
            return ((ICollection<KeyValuePair<T, string>>)this.字典).Contains(item);
        }

        public bool ContainsKey(T key)
        {
            return ((IDictionary<T, string>)this.字典).ContainsKey(key);
        }

        void ICollection<KeyValuePair<T, string>>.CopyTo(KeyValuePair<T, string>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<T, string>>)this.字典).CopyTo(array, arrayIndex);
        }

        IEnumerator<KeyValuePair<T, string>> IEnumerable<KeyValuePair<T, string>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<T, string>>)this.字典).GetEnumerator();
        }

        bool IDictionary<T, string>.Remove(T key)
        {
            return ((IDictionary<T, string>)this.字典).Remove(key);
        }

        bool ICollection<KeyValuePair<T, string>>.Remove(KeyValuePair<T, string> item)
        {
            return ((ICollection<KeyValuePair<T, string>>)this.字典).Remove(item);
        }

        public bool TryGetValue(T key, [MaybeNullWhen(false)] out string value)
        {
            return ((IDictionary<T, string>)this.字典).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.字典).GetEnumerator();
        }
    }
}
