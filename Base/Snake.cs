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
        private List<Position> snakeBody;

        public List<Position> SnakeBody => snakeBody;

        private Animation idleAnimation;

        private Food food;

        private Enemy enemy;

        public event Action onDead;
        
        public int x { get; set; }
        public int y { get; set; }

        char dir = 'r';

        public Map map = new Map();

        private int snakeScore = 0; 

        public Snake(Vector2 pos, Food food, Enemy enemy) : base(pos)
        {

            Engine.LoadImage("assets/SnakeBody.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            snakeBody = new List<Position>();

            snakeBody.Add(new Position(pos));

            renderer = new Renderer(currentAnimation);

            this.food = food;

            this.enemy = enemy;

            x = (int)pos.x;
            y = (int)pos.y;

            onDead += GameManager.Instance.snakeDead;
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
            snakeGrow(food);
            isDead();
            deadByEnemy(enemy);
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

            snakeBody.Add(new Position(new Vector2(x, y)));
            snakeBody.RemoveAt(0);
        }

        public void shoot()
        {

        }

        public void snakeGrow (Food f)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];
            Position food = f.foodLocation();

            int scale = 10;
            int bodyGrowth = 2;

            float distanceX = Math.Abs((snakeHead.Transform.x + (scale / 2)) - (food.Transform.x + (scale / 2)));
            float distanceY = Math.Abs((snakeHead.Transform.y + (scale / 2)) - (food.Transform.y + (scale / 2)));

            if (distanceX <= scale && distanceY <= scale)
            {
                for (int i = 0; i < bodyGrowth; i++ )
                {
                    snakeBody.Add(new Position(new Vector2(x,y)));
                }

                f.foodNewLocation();

                this.food = f;

                snakeScore++;

                GameManager.Instance.Score = snakeScore;
            }
        }

        public void deadByEnemy(Enemy e)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            Position enemy = e.enemyLocation();

            int scale = 10;

            float distanceXenemy = Math.Abs((snakeHead.Transform.x + (scale / 2)) - (enemy.Transform.x + (scale / 2)));
            float distanceYenemy = Math.Abs((snakeHead.Transform.y + (scale / 2)) - (enemy.Transform.y + (scale / 2)));

            if (distanceXenemy <= scale && distanceYenemy <= scale)
            {
                onDead.Invoke();
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
                    onDead.Invoke();
                }

            }
        }

        public void hitWall(Map map)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            if (snakeHead.Transform.x >= map.Width || snakeHead.Transform.x <= 0 || snakeHead.Transform.y >= map.Height || snakeHead.Transform.y <= 0 )
            {
                onDead.Invoke();
            }
        }

        public void restart()
        {
            snakeBody.Clear();
            x = 50;
            y = 50;
            snakeBody.Add(new Position(new Vector2(x, y)));
            GameManager.Instance.Dead = false;
            dir = 'r';
        }
    }

}
