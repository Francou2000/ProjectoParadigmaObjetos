using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GameManager
    {
        private static GameManager instance;
        public Snake snake;
        private int gameStatus = 0; //0 inicio, 1 juego, 2 victoria, 3 derrota
        private IntPtr mainMenuScreen = Engine.LoadImage("assets/MainMenu.png");
        private IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        private IntPtr gameOverScreen = Engine.LoadImage("assets/GameOver.png");

        public int score { get; set; } = 0;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        public void Update()
        {
            switch (gameStatus)
            {
                case 0:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        gameStatus = 1;
                    }
                    break;
                case 1:
                    Program.Update();
                    break;
                case 2:
                    if (score == 10)
                    {
                        gameStatus = 2;
                    }
                    break;
                case 3:
                    if (snake.isAlive == false)
                    {
                        gameStatus = 3;
                    }
                    break;
            }

        }

        public void ChangeGameStatus(int gs)
        {
            gameStatus = gs;
        }

        public void Render()
        {
            Engine.Clear();
            switch (gameStatus)
            {
                case 0:
                    Engine.Draw(mainMenuScreen, 0, 0);
                    break;
                case 1:
                    Program.Render();
                    break;
                case 2:
                    Engine.Draw(winScreen, 0, 0);
                    break;
                case 3:
                    Engine.Draw(gameOverScreen, 0, 0);
                    break;
            }
            Engine.Show();
        }
    }
}
