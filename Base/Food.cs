using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
    public  class Food
    {
        public Position foodPosition = new Position();

        public static Snake snake = new Snake();
        private List<Position> snakeBody;

        Random random = new Random();

        public Map map = new Map();

        private Animation currentAnimation;
        private Animation idleAnimation;

        public Food()
        {
            foodPosition.x = random.Next(15, map.Width-15);
            foodPosition.y = random.Next(15, map.Height-15);

            Engine.LoadImage("assets/Food.png");
            CreateAnimations();
            currentAnimation = idleAnimation;
        }
        public void Update()
        {

            currentAnimation.Update();
        }
        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Food/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
        }

        public void drawFood()
        {
            Engine.Draw(currentAnimation.CurrentFrame , foodPosition.x, foodPosition.y);
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
