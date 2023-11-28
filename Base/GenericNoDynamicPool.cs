using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GenericNoDynamicPool <T>
    {
        public List<T> itemsInUse = new List<T>();

        public List<T> itemsAvailable = new List<T>();

        public GenericNoDynamicPool(int maxPool, T newItem)
        {
            for (int i = 0; i < maxPool; i++)
            {
                itemsAvailable.Add(newItem);
            }
        }

        public T GetItem()
        {
             T itemToReturn = default(T);

            if (itemsAvailable.Count > 0)
            {
                itemToReturn = itemsAvailable[0];
                itemsAvailable.RemoveAt(0);
                itemsInUse.Add(itemToReturn);
            }
            
            return itemToReturn;
        }

        public void RecycleItem(T item)
        {
            itemsInUse.Remove(item);
            itemsAvailable.Add(item);
        }
    }
}
