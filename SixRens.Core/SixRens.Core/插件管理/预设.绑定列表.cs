using System.Collections;

namespace SixRens.Core.插件管理
{
    public sealed partial class 预设
    {
        private class 绑定列表<T> : I可批量操作列表<T>
        {
            protected readonly List<T> 列表;
            protected readonly 预设 预设;
            internal 绑定列表(预设 预设, IList<T>? 数据 = null)
            {
                this.列表 = 数据 is null ? new() : new(数据);
                this.预设 = 预设;
            }
            private void 触发事件()
            {
                this.预设.预设被修改?.Invoke(this.预设, EventArgs.Empty);
            }

            public T this[int index]
            {
                get => ((IList<T>)this.列表)[index];
                set
                {
                    ((IList<T>)this.列表)[index] = value;
                    this.触发事件();
                }
            }

            public int Count => ((ICollection<T>)this.列表).Count;

            public bool IsReadOnly => ((ICollection<T>)this.列表).IsReadOnly;

            public void Add(T item)
            {
                ((ICollection<T>)this.列表).Add(item);
                this.触发事件();
            }

            public void Clear()
            {
                ((ICollection<T>)this.列表).Clear();
                this.触发事件();
            }

            public bool Contains(T item)
            {
                return ((ICollection<T>)this.列表).Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                ((ICollection<T>)this.列表).CopyTo(array, arrayIndex);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return ((IEnumerable<T>)this.列表).GetEnumerator();
            }

            public int IndexOf(T item)
            {
                return ((IList<T>)this.列表).IndexOf(item);
            }

            public void Insert(int index, T item)
            {
                ((IList<T>)this.列表).Insert(index, item);
                this.触发事件();
            }

            public bool Remove(T item)
            {
                if (((ICollection<T>)this.列表).Remove(item))
                {
                    this.触发事件();
                    return true;
                }
                return false;
            }

            public void RemoveAt(int index)
            {
                ((IList<T>)this.列表).RemoveAt(index);
                this.触发事件();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this.列表).GetEnumerator();
            }

            public void AddMany(IEnumerable<T> values)
            {
                this.列表.AddRange(values);
                this.触发事件();
            }

            public void InsertMany(int index, IEnumerable<T> values)
            {
                this.列表.InsertRange(index, values);
                this.触发事件();
            }

            public void ReplaceAll(IEnumerable<T> values)
            {
                this.列表.Clear();
                this.列表.AddRange(values);
                this.触发事件();
            }
        }
    }
}
