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

            GameManager.Instance.LevelController.GameObjectsList.Add(new Enemy(new Vector2(200, 200)));
            GameManager.Instance.LevelController.GameObjectsList.Add(FoodFactory.CreateFood(FoodType.Static, new Vector2(100,100)));
            GameManager.Instance.LevelController.GameObjectsList.Add(FoodFactory.CreateFood(FoodType.Horizontal, new Vector2(300, 300)));

            player = new Snake(new Vector2(50,50));
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
