using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Renderer
    {
        private Animation animation;

        public Renderer(Animation anim)
        {

            animation = anim;
        }

        public void Render(Position position)
        {
            Engine.Draw(animation.CurrentFrame, position.Transform.x, position.Transform.y);
        }
    }
}
