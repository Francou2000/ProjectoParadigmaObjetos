

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Tao.Sdl;

namespace MyGame
{

    class Program
    {
        static void Main(string[] args)
        {
            Engine.Initialize();

            Map map = new Map();
            Snake snake = new Snake();
            Food food = new Food();

            while (true)
            {

                Engine.Clear();
          
                Engine.Show();

                food.drawFood();
                snake.drawSnake();
                snake.moveSnake();
                snake.snakeGrow(food.foodLocation(), food);
                snake.isDead();
                

                Sdl.SDL_Delay(20);
            }
        }

    }

}
