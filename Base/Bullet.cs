﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet : GameObject
    {
        public Position bulletPosition = new Position();

        public Map map = new Map();

        private Enemy enemy;

        private Animation idleAnimation;

        public Bullet(Vector2 pos) : base(pos)
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

        public Position bulletLocation()
        {
            return bulletPosition;
        }

        public void killEnemy(Enemy e)
        {
            Position enemy = e.enemyLocation();

            int scale = 10;

            float distanceX = Math.Abs((bulletPosition.Transform.x + (scale / 2)) - (enemy.Transform.x + (scale / 2)));
            float distanceY = Math.Abs((bulletPosition.Transform.y + (scale / 2)) - (enemy.Transform.y + (scale / 2)));

            if (distanceX <= scale && distanceY <= scale)
            {
                e.newEnemy();

                this.enemy = e;
            }
        }
    }
}
