using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridRunner
{
    public enum Case { Grid, Runner }
    public class BootLoader : MonoBehaviour
    {
        public Case Case = Case.Grid;

        void Start()
        {
            GameObject game;
            switch (Case)
            {
                case Case.Grid:
                    game = Object.Instantiate(Resources.Load<GameObject>($"Levels/GridGame"));
                    game.name = "--->GRID GAME";
                    break;
                case Case.Runner:
                    game = Object.Instantiate(Resources.Load<GameObject>($"Levels/RunnerGame"));
                    game.name = "--->RUNNER GAME";
                    break;
            }


        }
    }
}
