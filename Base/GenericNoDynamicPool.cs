﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GenericNoDynamicPool <T>
    {
        private List<T> itemsInUse = new List<T>();
        private List<T> itemsAvailable = new List<T>();

        public GenericNoDynamicPool(int maxPool, T newItem)
        {
            for (int i = 0; i < maxPool; i++)
            {
                itemsAvailable.Add(newItem);
            }
        }

        public T GetItem(T t)
        {
             t = null;

            if (itemsAvailable.Count > 0)
            {
                itemToReturn = itemsAvailable[0];
                itemsAvailable.RemoveAt(0);
                itemsAvailable.Add(itemToReturn);
            }

            return t;
        }

        private void RecycleItem(T item)
        {
            itemsInUse.Remove(item);
            itemsAvailable.Add(item);
        }
    }
}
