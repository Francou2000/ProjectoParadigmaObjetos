using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelController
    {
        public List<GameObject> GameObjectsList = new List<GameObject>();

        private Snake player;

        public Snake Player => player;

        private Food food;

        public Food Food => food;

        private Time time;


        public void Initialization()
        {
            time.Initialize();

            player = new Snake(new Position(20,20));

            food = new Food(new Position(50,50));
        }
        public void Update()
        {
            time.Update();

            player.Update();

            food.Update();
        }



        public void Render()
        {
            Engine.Clear();

            player.Render();

            food.Render();

            Engine.Show();
        }
    }
}
