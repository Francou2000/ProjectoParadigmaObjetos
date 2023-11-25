using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy : GameObject, IDamageable
    {
        public Position enemyPosition = new Position();

        public Map map = new Map();

        Random random = new Random();

        private Animation idleAnimation;

        public Enemy(Vector2 pos): base(pos)
        {
            Engine.LoadImage("assets/EnemyBase.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            enemyPosition.Transform = pos;
            renderer = new Renderer(currentAnimation);
        }

        public override void Update()
        {
            currentAnimation.Update();
        }

        protected override void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Enemy/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
        }

        public override void Render()
        {
            renderer.Render(enemyPosition);
        }

        public Position enemyLocation()
        {
            return enemyPosition;
        }

        public void GetDamage()
        {
            enemyPosition.Transform = new Vector2(random.Next(15, map.Width - 15), random.Next(15, map.Height - 15));
            SpawnNewEnemy();
        }

        public void SpawnNewEnemy()
        {
            
        }
    }
}
