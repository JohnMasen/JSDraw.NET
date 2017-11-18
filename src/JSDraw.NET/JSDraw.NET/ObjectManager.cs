using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace JSDraw.NET
{
    public class ObjectManager<TTag> where TTag : class
    {
        int id = 0;
        internal IDictionary<int, IManaged<TTag>> Items { get; private set; } = new SortedDictionary<int, IManaged<TTag>>();
        public ManagedItem<T, TTag> Add<T>(T obj,bool isPersistent=false,TTag tag =null)
        {
            ManagedItem<T, TTag> result = new ManagedItem<T, TTag>(id, obj, isPersistent, tag);
            Items.Add(id++, result);
            return result;
        }
        public void Remove(int id)
        {
            Items.Remove(id);
        }

        public ManagedItem<T, TTag> Get<T>(int id)
        {
            return Items[id] as ManagedItem<T, TTag>;
        }

        public T GetItem<T>(int id)
        {
            return Get<T>(id).Item;
        }

        public void Clear(bool keepPersistentItems=true)
        {
            if (keepPersistentItems)
            {
                var tmp = (from item in Items
                           where !item.Value.IsPersistent
                           select item.Key).ToList();
                tmp.ForEach(x => Items.Remove(x));
                
            }
            else
            {
                Items.Clear();

            }
            if (Items.Count==0)
            {
                id = 0;
            }
            else
            {
                id = Items.Max(x => x.Key) + 1;
            }
        }
    }
}
