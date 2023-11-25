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
            
            player = new Snake(new Vector2(50,50));

            //GameObjectsList.Add(Enemyfactory.CreateEnemy(EnemyType.Fast, new Vector2(700, 300)));
        }

        public void Update()
        {
            time.Update();

            player.Update();

            for (int i = 0; i < GameObjectsList.Count; i++)
            {
                GameObjectsList[i].Update();
            }
        }

        public void Render()
        {
            Engine.Clear();

            player.Render();

            for (int i = 0; i < GameObjectsList.Count; i++)
            {
                GameObjectsList[i].Render();
            }

            Engine.Show();
        }
    }
}
