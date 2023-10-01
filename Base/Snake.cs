using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Snake
    {
        private static List<Position> snakeBody;
        public List<Position> SnakeBody => snakeBody;

        public int x { get; set; }
        public int y { get; set; }

        char dir = 'r';

        static IntPtr image = Engine.LoadImage("assets/SnakeBody.png");

        public Map map = new Map();

        private int snakeScore = 0;


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

            int speed = 3;

            if (dir == 'u')
            {
                for (int i = 0; i < speed; i++)
                {
                    y--;
                }
            }
            else if (dir == 'd')
            {
                for (int i = 0; i < speed; i++)
                {
                    y++;
                }
            }
            else if (dir == 'r')
            {
                for (int i = 0; i < speed; i++)
                {
                    x++;
                }
            }
            else if (dir == 'l')
            {
                for (int i = 0; i < speed; i++)
                {
                    x--;
                }
            }

            snakeBody.Add(new Position(x, y));
            snakeBody.RemoveAt(0);
        }

        public void snakeGrow(Position food, Food f)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            int scale = 10;
            int bodyGrowth = 2;

            float distanceX = Math.Abs((snakeHead.x + (scale / 2)) - (food.x + (scale / 2)));
            float distanceY = Math.Abs((snakeHead.y + (scale / 2)) - (food.y + (scale / 2)));

            if (distanceX <= scale && distanceY <= scale)
            {
                for (int i = 0; i < bodyGrowth; i++ )
                {
                    snakeBody.Add(new Position(x, y));
                }

                f.foodNewLocation();

                Engine.Debug("score");

                snakeScore++;

                GameManager.Instance.Score = snakeScore;
            }
        }

        public void isDead()
        {
            Position snakeHead = snakeBody[0];

            for (int i = 10; i < snakeBody.Count; i++)
            {
                Position snake = snakeBody[i];

                int scale = 10;

                float distanceX = Math.Abs((snakeHead.x + (scale / 2)) - (snake.x + (scale / 2)));
                float distanceY = Math.Abs((snakeHead.y + (scale / 2)) - (snake.y + (scale / 2)));

                if (distanceX <= scale && distanceY <= scale)
                {
                    GameManager.Instance.GameStatus = 3;
                    break;
                }
            }
        }

        public void hitWall(Map map)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            if (snakeHead.x >= map.Width || snakeHead.x <= 0 || snakeHead.y >= map.Height || snakeHead.y <= 0 )
            {
                GameManager.Instance.GameStatus = 3;
            }
        }
    }
}
