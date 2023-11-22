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

        private Enemy enemy;

        public Enemy Enemy => enemy;

        private Time time;


        public void Initialization()
        {
            time.Initialize();

            food = new Food(new Vector2(100,100));

            enemy = new Enemy(new Vector2(200, 200));
            
            player = new Snake(new Vector2(50,50), food, enemy);
        }

        public void Update()
        {
            time.Update();

            player.Update();

            food.Update();

            enemy.Update();
        }

        public void Render()
        {
            Engine.Clear();

            player.Render();

            food.Render();

            enemy.Render();

            Engine.Show();
        }
    }
}
