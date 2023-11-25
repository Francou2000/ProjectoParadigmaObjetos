using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
    public class Food : GameObject, IFoodable
    {
        public Position foodPosition = new Position();

        Random random = new Random();

        public Map map = new Map();

        private Animation idleAnimation;

        private float speed;

        public Food(Vector2 pos, float speed) : base(pos)
        {
            Engine.LoadImage("assets/Food.png");
            CreateAnimations();
            currentAnimation = idleAnimation;

            this.speed = speed;

            foodPosition.Transform = pos;
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
                IntPtr frame = Engine.LoadImage($"assets/Food/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
        }

        public override void Render()
        {
            renderer.Render(foodPosition);
        }

        private void MoveFood()
        {
            Position.Translate(new Vector2(1, 0), speed);

            if (Position.Transform.x > 1000)
            {
                foodPosition.Transform = new Vector2(0 - 10, Position.Transform.y);
            }
        }


        public Position foodLocation()
        {
            return foodPosition;
        }

        public void GetFood()
        {
            foodPosition.Transform = new Vector2 (random.Next(15, map.Width - 15), random.Next(15, map.Height - 15));               
        }
    }
}
