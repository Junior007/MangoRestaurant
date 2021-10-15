using mango.product.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mango.product.data.Infrastructure.ChangeCache
{
    internal class ItemChanged<T> where T:IEntity
    {

        public ItemChanged(int id, T item)
        {
            Key = id;
            Item = item;
        }
        public int Key { get; internal set; }
        public T Item { get; internal set; }

    }
}
