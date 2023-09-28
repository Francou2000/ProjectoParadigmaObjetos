

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

        private static DateTime _startTime;
        private static float _lastTimeFrame;
        public static float DeltaTime;

        static void Main(string[] args)
        {
            Initialize();

            while (true)
            {
                GameManager.Instance.Update();
                GameManager.Instance.Render();

            }
        }

        public static void Initialize()
        {
            Engine.Initialize(500, 500, 24);

            _startTime = DateTime.Now;
        }

        public static void Update()
        {
            snake.moveSnake();
            snake.snakeGrow(food.foodLocation(), food);
            snake.isDead();
            snake.hitWall(map);
        }

        public static void Render()
        {
            Engine.Clear();

            float currentTime = (float)(DateTime.Now - _startTime).TotalSeconds;
            DeltaTime = currentTime - _lastTimeFrame;
            _lastTimeFrame = currentTime;

            food.drawFood();
            snake.drawSnake();

            Engine.Show();

            Sdl.SDL_Delay(20);

        }

    }
}
