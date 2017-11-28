using System;
using System.Collections.Generic;
using System.Text;

namespace JSDraw.NET
{
    public class ManagedItem<T,TTag>:IManaged<TTag>
    {
        public int ID { get; private set; }
        public T Item { get; set; }
        public bool IsPersistent { get; set; }
        public TTag Tag { get; set; }
        public ManagedItem(int id,T obj,bool isPersistent, TTag tag)
        {
            this.ID = id;
            Item = obj;
            IsPersistent = isPersistent;
            Tag = tag;
        }
        //public int Compare(IManaged x, IManaged y)
        //{
        //    return Comparer<int>.Default.Compare(x.ID, y.ID);
        //}
    }
}
