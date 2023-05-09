using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridRunner
{
    public enum Case { Grid, Runner }
    public class BootLoader : MonoBehaviour
    {
        [Header("Choose Game Type:")]
        public Case Case = Case.Grid;

        [Header("Game Types:")]
        [SerializeField] GameObject m_GridGame;
        [SerializeField] GameObject m_RunnerGame;


        void Start()
        {
            GameObject game;
            switch (Case)
            {
                case Case.Grid:
                    game = Instantiate(m_GridGame);
                    game.name = "--->GRID GAME";
                    break;
                case Case.Runner:
                    game = Instantiate(m_RunnerGame);
                    game.name = "--->RUNNER GAME";
                    break;
            }


        }
    }
}
