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

        Random random = new Random();

        Map map = new Map();

        static IntPtr image = Engine.LoadImage("assets/Food.png");

        private int scaleX = 10;
        private int scaleY = 10;

        public Food()
        {
            foodPosition.x = random.Next(5, map.Width);
            foodPosition.y = random.Next(5, map.Height);
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
            foodPosition.x = random.Next(5, map.Width);
            foodPosition.y = random.Next(5, map.Height);
        }
    }
}
