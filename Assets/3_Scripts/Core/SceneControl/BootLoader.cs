using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridRunner
{
    public enum Case { Grid, Runner }
    public class BootLoader : AbstractSingleton<BootLoader>
    {
        private GameObject mainMenu;
        private Case Case = Case.Grid;
        private GameObject game;

        private void Start()
        {
            mainMenu = transform.GetChild(0).gameObject;
            /*
            switch (Case)
            {
                case Case.Grid:
                    Object.Instantiate(Resources.Load<GameObject>($"Levels/GridGame")).name = "--->GRID GAME";
                    break;
                case Case.Runner:
                    Object.Instantiate(Resources.Load<GameObject>($"Levels/RunnerGame")).name = "--->RUNNER GAME";
                    break;
            }
            */
        }
        private void DeactivateMenu(GameObject gameObj)
        {
            game = gameObj;
            mainMenu.SetActive(false);
        }

        public void ActivateMainMenu()
        {
            mainMenu.SetActive(true);
            Destroy(game);
        }

        public void _CreateGridGame()
        {
            GameObject gameObj = Object.Instantiate(Resources.Load<GameObject>($"Levels/GridGame"));
            gameObj.name = "--->GRID GAME";
            DeactivateMenu(gameObj);
        }

        public void _CreateRunnerGame()
        {
            GameObject gameObj = Object.Instantiate(Resources.Load<GameObject>($"Levels/RunnerGame"));
            gameObj.name = "--->RUNNER GAME";
            DeactivateMenu(gameObj);
        }
    }
}
