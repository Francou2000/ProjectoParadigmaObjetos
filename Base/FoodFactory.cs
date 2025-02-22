﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum FoodType
    {
        Static, Horizontal
    }
    public static class FoodFactory
    {
        public static Food CreateFood(FoodType type, Vector2 position)
        {
            switch (type)
            {
                case FoodType.Static:
                    return new Food(position, 0);
                case FoodType.Horizontal:
                    return new Food(position, 100);
            }
            return null;
        }
    }
}
