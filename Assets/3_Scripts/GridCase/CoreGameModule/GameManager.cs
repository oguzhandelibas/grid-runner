using GridRunner.Enums;
using UnityEngine;
using GridRunner.Grid.CoreGameModule.Signals;

namespace GridRunner.Grid.CoreGameModule
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameStates gameStates;
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            OnChangeGameState(GameStates.GridGame);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnChangeGameState(GameStates _gameStates)
        {
            gameStates = _gameStates;
            switch (gameStates)
            {
                case GameStates.Default:
                    break;
                case GameStates.GridGame:
                    CoreGameSignals.Instance.onReset?.Invoke();
                    break;
            }
        }

        public void ChangeGameStateButton(GameStates _gameStates)
        {
            OnChangeGameState(_gameStates);
        }
    }
}
