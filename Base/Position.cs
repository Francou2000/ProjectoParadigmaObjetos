using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Position
    {
        private Vector2 position= new Vector2(0);
        public Vector2 Transform { get { return position; } set { position = value; } }

        
        public Position()
        {
        }
        
        public Position(Vector2 position)
        {
            this.position = position;
        }

        public void Translate(Vector2 direction, float speed)
        {
            position.x += direction.x * speed * Time.DeltaTime;
            position.y += direction.y * speed * Time.DeltaTime;
        }
    }
}
