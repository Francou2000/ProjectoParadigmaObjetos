﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public struct Vector2
    {
        public float x;
        public float y;



        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(float value)
        {
            this.x = value;
            this.y = value;
        }
        
    }
}
