using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GenericDynamicPool <T>
    {
        private List<T> itemsInUse = new List<T>();
        private List<T> itemsAvailable = new List<T>();

        public T GetItem(T t)
        {
            T newItem;

            if (itemsAvailable.Count > 0)
            {
                newItem = itemsAvailable[0];
                itemsAvailable.RemoveAt(0);
            }
            else 
            {
                newItem = t;
            }

            itemsInUse.Add(newItem);
            return newItem;
        }

        private void RecycleItem(T item)
        {
            itemsInUse.Remove(item);
            itemsAvailable.Add(item);
        }
    }
}
