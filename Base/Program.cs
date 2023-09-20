

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
            Engine.Initialize(500,500,24);

            Map map = new Map();
            Snake snake = new Snake();
            Food food = new Food();

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
