using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum GameStatus
    {
        mainMenu, level, victory, defeat
    }
    public class GameManager
    {
        private static GameManager instance;

        private GameStatus gameStatus = GameStatus.mainMenu;

        private LevelController levelController;
        public LevelController LevelController => levelController;

        private int score = 0;
        public int Score
        {
            get 
            {
                return this.score;
            }

            set 
            { 
                this.score = value; 
            }
        }

        private bool dead = false;
        public bool Dead
        {
            get
            {
                return this.dead;
            }

            set
            {
                this.dead = value;
            }
        }

        private IntPtr mainMenuScreen = Engine.LoadImage("assets/MainMenu.png");
        private IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        private IntPtr gameOverScreen = Engine.LoadImage("assets/GameOver.png");

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

        public void Initialize()
        {
            Engine.Initialize(500, 500, 24);
            levelController = new LevelController();
            levelController.Initialization();
        }

        public void Update()
        {
            switch (gameStatus)
            {
                case GameStatus.mainMenu:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        ChangeGameStatus(GameStatus.level);
                    }
                    break;
                case GameStatus.level:
                    LevelController.Update();
                    if (score == 100)
                    {
                        gameStatus = GameStatus.victory;
                    }
                    if (dead == true)
                    {
                        gameStatus = GameStatus.defeat;
                    }
                    break;
                case GameStatus.victory:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        ChangeGameStatus(GameStatus.level);
                    }
                    break;
                case GameStatus.defeat:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        ChangeGameStatus(GameStatus.level);
                    }
                    break;
            }

        }

        public void ChangeGameStatus(GameStatus gs)
        {
            gameStatus = gs;
        }

        public void Render()
        {
            Engine.Clear();
            switch (gameStatus)
            {
                case GameStatus.mainMenu:
                    Engine.Draw(mainMenuScreen, 0, 0);
                    break;
                case GameStatus.level:
                    LevelController.Render();
                    break;
                case GameStatus.victory:
                    Engine.Draw(winScreen, 0, 0);
                    break;
                case GameStatus.defeat:
                    Engine.Draw(gameOverScreen, 0, 0);
                    break;
            }
            Engine.Show();
        }
    }
}
