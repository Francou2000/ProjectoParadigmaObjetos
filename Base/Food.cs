using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public  class Food
    {
        public Position foodPosition = new Position();

        public static Snake snake = new Snake();
        private List<Position> snakeBody;

        Random random = new Random();

        Map map = new Map();

        static IntPtr image = Engine.LoadImage("assets/Food.png");

        public Food()
        {
            foodPosition.x = random.Next(15, map.Width-15);
            foodPosition.y = random.Next(15, map.Height-15);
        }

        public void drawFood()
        {
            Engine.Draw(image, foodPosition.x, foodPosition.y);
        }

        public Position foodLocation()
        {
            return foodPosition;
        }

        public void foodNewLocation()
        {
            bool isSnake = true;

            snakeBody = snake.SnakeBody;

            while (isSnake)
            {
                isSnake = false;

                
                foodPosition.x = random.Next(15, map.Width - 15);
                foodPosition.y = random.Next(15, map.Height - 15);

                foreach (Position position in snakeBody)
                {
                    if (position.x == foodPosition.x && position.y == foodPosition.y)
                    {
                        isSnake = true;
                    }
                }
            }
                
        }
    }
}
