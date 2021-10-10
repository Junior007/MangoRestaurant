using mango.product.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace mango.product.data.Infrastructure.ChangeCache
{
    internal class ItemsChanged<T> where T : IEntity
    {
        private readonly List<ItemChanged<T>> updatedItems = new List<ItemChanged<T>>();


        public T GetScope(Guid key)
        {
            var itemScoped = updatedItems.FirstOrDefault(x => x.Key == key);
            if (itemScoped != null)
            {
                return  itemScoped.Item;
                
            }
            return default(T);
        }


        public Guid SetScope(T entity)
        {

            Guid key;

            var itemChanged = updatedItems.FirstOrDefault(e => e.Item.Equals(entity));




            if (itemChanged==null)
            {
                key = Guid.NewGuid();
                updatedItems.Add(new ItemChanged<T>(key, entity));
            }
            else
            {
                var item = updatedItems.FirstOrDefault(e => e.Item.Equals(entity));
                key = item.Key;
            }

            return key;
        }
    }

}
