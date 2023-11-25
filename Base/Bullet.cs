using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet : GameObject
    {
        public Position bulletPosition = new Position();

        private Snake snake;

        public Map map = new Map();

        private Enemy enemy;

        private Animation idleAnimation;

        private int speed = 0;

        public Bullet(Vector2 pos, int speed) : base(pos)
        {
            Engine.LoadImage("assets/BulletBase.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            bulletPosition.Transform = pos;
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
            renderer.Render(bulletPosition);
        }

        private void Move()
        {
            position.Translate(new Vector2(0, -1), speed);

            if (position.Transform.y < 0)
            {
                DestroyBullet();
            }
        }

        public Position bulletLocation()
        {
            return bulletPosition;
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

        }
    }
}
