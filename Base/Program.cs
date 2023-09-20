

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Tao.Sdl;

namespace MyGame
{

    class Program
    {    
        
        public static Map map = new Map();
        public static Snake snake = new Snake();
        public static Food food = new Food();


        static void Main(string[] args)
        {
            Engine.Initialize(500,500,24);



            while (true)
            {

                Engine.Clear();
          


                food.drawFood();
                snake.drawSnake();
                snake.moveSnake();
                snake.snakeGrow(food.foodLocation(), food);
                snake.isDead();

                Engine.Show();


                Sdl.SDL_Delay(20);
            }
        }

    }

}
