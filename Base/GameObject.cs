using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class GameObject
    {
        protected Position position;
        protected Renderer renderer;
        public Position Position => position;

        protected Animation currentAnimation;

        public GameObject(Position pos)
        {
            position = new Position(pos.x, pos.y);

            CreateAnimations();

            renderer = new Renderer(currentAnimation);
        }

        protected virtual void CreateAnimations()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }


    }
}

