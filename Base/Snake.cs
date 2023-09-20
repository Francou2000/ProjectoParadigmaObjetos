using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Snake
    {
        List<Position> snakeBody;

        public int x { get; set; }
        public int y { get; set; }

        char dir = 'r';

        static IntPtr image = Engine.LoadImage("assets/SnakeBody.png");

        public Snake()
        {
            x = 20;
            y = 20;

            snakeBody = new List<Position>();

            snakeBody.Add(new Position(x, y));
        }

        public void drawSnake()
        {
            foreach (Position position in snakeBody) 
            {
                Engine.Draw(image, position.x, position.y);
            }
           
        }

        public void Input()
        {
            if (Engine.KeyPress(Engine.KEY_LEFT) && dir != 'r') 
            {
                dir = 'l';
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT) && dir != 'l') 
            {
                dir = 'r';
            }

            if (Engine.KeyPress(Engine.KEY_UP) && dir != 'd') 
            {
                dir = 'u';
            }

            if (Engine.KeyPress(Engine.KEY_DOWN) && dir != 'u') 
            {
                dir = 'd';
            }
        }

        public void moveSnake()
        {
            Input();

            if (dir == 'u')
            {
                y--;
            }
            else if (dir == 'd')
            {
                y++;
            }
            else if (dir == 'r')
            {
                x++;
            }
            else if (dir == 'l')
            {
                x--;
            }

            snakeBody.Add(new Position(x, y));
            snakeBody.RemoveAt(0);
        }

        public void snakeGrow(Position food, Food f)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            int scaleX = 10;
            int scaleY = 10;

            float distanceX = Math.Abs(snakeHead.x + (scaleX / 2) - food.x + (scaleX / 2));
            float distanceY = Math.Abs(snakeHead.y + (scaleY / 2) - food.y + (scaleY / 2));

            if (distanceX <= scaleX && distanceY <= scaleY) //snakeHead.x == food.x && snakeHead.y == food.y
            {
                snakeBody.Add(new Position(x, y));
                f.foodNewLocation();
            }
        }

        public void isDead()
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            for (int i = 0; i < snakeBody.Count - 2; i++)
            {
                Position snake = snakeBody[i];

                if(snakeHead.x == snake.x && snakeHead.y == snake.y)
                {
                    //muerte
                }
            }
        }

        public void hitWall(Map map)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            if (snakeHead.x >= map.Width || snakeHead.x <= 0 || snakeHead.y >= map.Height || snakeHead.y <= 0 )
            {
               //muerte
            }
        }
    }
}
