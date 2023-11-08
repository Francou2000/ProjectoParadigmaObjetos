using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyGame
{
    public class Snake : GameObject
    {
        private static List<Position> snakeBody;

        public List<Position> SnakeBody => snakeBody;

        private Animation idleAnimation;

        private Food food;

        public Food Food => food;

        public int x { get; set; }
        public int y { get; set; }

        char dir = 'r';

        public Map map = new Map();

        private int snakeScore = 0; 

        public Snake(Vector2 pos) : base(pos)
        {

            Engine.LoadImage("assets/SnakeBody.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            snakeBody = new List<Position>();

            snakeBody.Add(new Position());
            renderer = new Renderer(currentAnimation);
        }

        protected override void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();

                for (int i = 0; i < 4; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Snake/Idle/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
                currentAnimation = idleAnimation;
            
        }

        public override void Render()
        {
            foreach (Position position in snakeBody)
            {
                renderer.Render(position);
            }
        }

    

        public override void Update()
        {
            currentAnimation.Update();

            moveSnake();
            snakeGrow(food.foodLocation(), food);
            isDead();
            hitWall(map);
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

            snakeBody.Add(new Position());
            snakeBody.RemoveAt(0);
        }

        public void snakeGrow(Position food, Food f)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            int scale = 10;
            int bodyGrowth = 2;

            float distanceX = Math.Abs((snakeHead.Transform.x + (scale / 2)) - (food.Transform.x + (scale / 2)));
            float distanceY = Math.Abs((snakeHead.Transform.y + (scale / 2)) - (food.Transform.y + (scale / 2)));

            if (distanceX <= scale && distanceY <= scale)
            {
                for (int i = 0; i < bodyGrowth; i++ )
                {
                    snakeBody.Add(new Position());
                }

                f.foodNewLocation();

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

                float distanceX = Math.Abs((snakeHead.Transform.x + (scale / 2)) - (snake.Transform.x + (scale / 2)));
                float distanceY = Math.Abs((snakeHead.Transform.y + (scale / 2)) - (snake.Transform.y + (scale / 2)));

                if (distanceX <= scale && distanceY <= scale)
                {
                    GameManager.Instance.Dead = true;
                }
            }
        }

        public void hitWall(Map map)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            if (snakeHead.Transform.x >= map.Width || snakeHead.Transform.x <= 0 || snakeHead.Transform.y >= map.Height || snakeHead.Transform.y <= 0 )
            {
                GameManager.Instance.Dead = true;
            }
        }
    }
}
