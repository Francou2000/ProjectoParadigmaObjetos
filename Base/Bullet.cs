using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet : GameObject
    {
        public Map map = new Map();

        private Animation idleAnimation;

        private int speed = 10;

        private char dir = 'r';

        public char Dir { set { dir = value; } }   

        public Bullet(Vector2 pos, int speed) : base(pos)
        {
            Engine.LoadImage("assets/BulletBase.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            position.Transform = pos;

            this.speed = speed;
            renderer = new Renderer(currentAnimation);
        }

        public override void Update()
        {
            currentAnimation.Update();
            Move();
            CheckCollisions();
        }

        protected override void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Bullet/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
        }

        public override void Render()
        {
            renderer.Render(position);
        }

        private void Move()
        {
            if (dir == 'u')
            {
                position.Translate(new Vector2(0, -1), speed);
            }

            if (dir == 'd')
            {
                position.Translate(new Vector2(0, 1), speed);
            }

            if (dir == 'l')
            {
                position.Translate(new Vector2(-1, 0), speed);
            }

            if (dir == 'r')
            {
                position.Translate(new Vector2(1, 0), speed);
            }

            if (position.Transform.y < 0 || position.Transform.x < 0 || position.Transform.y > 500 || position.Transform.x > 500)
            {
                DestroyBullet();
            }
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < GameManager.Instance.LevelController.GameObjectsList.Count; i++)
            {
                GameObject obj = GameManager.Instance.LevelController.GameObjectsList[i];

                if (obj is IDamageable objDamage)
                {
                    int scale = 10;

                    float distanceX = Math.Abs((obj.Position.Transform.x + (scale / 2)) - (Position.Transform.x + (scale / 2)));
                    float distanceY = Math.Abs((obj.Position.Transform.y + (scale / 2)) - (Position.Transform.y + (scale / 2)));

                    if (distanceX <= scale && distanceY <= scale)
                    {
                        objDamage.GetDamage();
                        DestroyBullet();
                    }
                }
            }
        }

        public void DestroyBullet()
        {
            GameManager.Instance.LevelController.Player.bulletsPool.RecycleItem(this);
            GameManager.Instance.LevelController.GameObjectsList.Remove(this);
        }
    }
}
