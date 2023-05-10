using UnityEngine;
using GridRunner.Enums;
using GridRunner.Runner.CoreGameModule.Signals;
using GridRunner.Runner.LevelModule.Signals;

namespace GridRunner.Runner.CoreGameModule
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
            OnChangeGameState(GameStates.Default);
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
                    Debug.Log("GameManager Default Initialization Level");
                    LevelSignals.Instance.onLevelInitialize?.Invoke();
                    break;
                case GameStates.RunnerGame:
                    CoreGameSignals.Instance.onReset?.Invoke();
                    break;
                case GameStates.Win:
                    LevelSignals.Instance.onLevelSuccessful?.Invoke();
                    break;
                case GameStates.Failed:
                    LevelSignals.Instance.onLevelFailed?.Invoke();
                    break;
            }
        }

        public void ChangeGameStateButton(GameStates _gameStates)
        {
            OnChangeGameState(_gameStates);
        }
    }
}
