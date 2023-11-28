using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyGame
{
    public class Snake : GameObject, IDeadable
    {
        private List<Position> snakeBody;

        public List<Position> SnakeBody => snakeBody;

        private Animation idleAnimation;

        private DateTime timeLastShoot;

        private float timeBetweenShoots = 1f;

        public GenericNoDynamicPool<Bullet> bulletsPool;

        public event Action onDead;
        
        public int x { get; set; }
        public int y { get; set; }

        public char dir = 'r';

        public Map map = new Map();

        public int snakeScore = 0;

        public int bulletSpeed = 300;

        public Snake(Vector2 pos) : base(pos)
        {

            Engine.LoadImage("assets/SnakeBody.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            snakeBody = new List<Position>();

            snakeBody.Add(new Position(pos));

            renderer = new Renderer(currentAnimation);

            x = (int)pos.x;
            y = (int)pos.y;

            bulletsPool = new GenericNoDynamicPool<Bullet>(1, new Bullet(new Vector2(0, 0), bulletSpeed));

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
            snakeGrow();
            isDead();
            deadByEnemy();
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

            if (Engine.KeyPress(Engine.KEY_C))
            {
                Shoot();
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

        private void Shoot()
        {
            DateTime currentTime = DateTime.Now;

            if ((currentTime - timeLastShoot).TotalSeconds >= timeBetweenShoots)
            {
                Bullet newBullet = bulletsPool.GetItem();
                if (newBullet != null)
                {
                    newBullet.Position.Transform = snakeBody[snakeBody.Count - 1].Transform;
                    newBullet.Dir = dir;
                    GameManager.Instance.LevelController.GameObjectsList.Add(newBullet);
                    timeLastShoot = currentTime;
                }
            }
        }

        public void snakeGrow ()
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            for (int i = 0; i < GameManager.Instance.LevelController.GameObjectsList.Count; i++)
            {
                GameObject obj = GameManager.Instance.LevelController.GameObjectsList[i];

                if (obj is IFoodable objDamage)
                {

                    int scale = 10;
                    int bodyGrowth = 2;

                    float distanceX = Math.Abs((obj.Position.Transform.x + (scale / 2)) - (snakeHead.Transform.x + (scale / 2)));
                    float distanceY = Math.Abs((obj.Position.Transform.y + (scale / 2)) - (snakeHead.Transform.y + (scale / 2)));

                    if (distanceX <= scale && distanceY <= scale)
                    {
                        for (int j = 0; j < bodyGrowth; j++)
                        {
                            snakeBody.Add(new Position(new Vector2(x, y)));
                        }

                        objDamage.GetFood();

                        snakeScore++;

                        GameManager.Instance.Score = snakeScore;

                    }
                }
            }
        }

        public void deadByEnemy()
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            for (int i = 0; i < GameManager.Instance.LevelController.GameObjectsList.Count; i++)
            {
                GameObject obj = GameManager.Instance.LevelController.GameObjectsList[i];

                if (obj is IDamageable objDamage)
                {
                    int scale = 10;

                    float distanceX = Math.Abs((obj.Position.Transform.x + (scale / 2)) - (snakeHead.Transform.x + (scale / 2)));
                    float distanceY = Math.Abs((obj.Position.Transform.y + (scale / 2)) - (snakeHead.Transform.y + (scale / 2)));

                    if (distanceX <= scale && distanceY <= scale)
                    {
                        GetDeath();
                    }
                }
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
                    GetDeath();
                }

            }
        }

        public void hitWall(Map map)
        {
            Position snakeHead = snakeBody[snakeBody.Count - 1];

            if (snakeHead.Transform.x >= map.Width || snakeHead.Transform.x <= 0 || snakeHead.Transform.y >= map.Height || snakeHead.Transform.y <= 0 )
            {
                GetDeath();
            }
        }

        public void GetDeath()
        {
            onDead.Invoke();
        }

        public void restart()
        {
            snakeBody.Clear();
            x = 50;
            y = 50;
            snakeBody.Add(new Position(new Vector2(x, y)));
            GameManager.Instance.Dead = false;
            dir = 'r';

            GameManager.Instance.LevelController.GameObjectsList.Clear();

            //bulletsPool.itemInUse.Clear();

            GameManager.Instance.LevelController.GameObjectsList.Add(new Enemy(new Vector2(200, 200)));
            GameManager.Instance.LevelController.GameObjectsList.Add(FoodFactory.CreateFood(FoodType.Static, new Vector2(100, 100)));
            GameManager.Instance.LevelController.GameObjectsList.Add(FoodFactory.CreateFood(FoodType.Horizontal, new Vector2(300, 300)));
        }
    }
}
