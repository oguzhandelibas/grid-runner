using GridRunner.Collectable.Enums;
using GridRunner.Collectable.Interfaces;
using GridRunner.Runner.CoreGameModule.Signals;
using GridRunner.Runner.LevelModule.Signals;
using UnityEngine;

namespace RunnerCollectableModule
{
    public class CollectablesManager : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private CollectableType collectableType;
        public CollectableType CollectableType()
        {
            return collectableType;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onRestartLevel += OnRestart;
            CoreGameSignals.Instance.onReset += OnRestart;
        }
        private void UnsbscribeEvents()
        {
            LevelSignals.Instance.onRestartLevel -= OnRestart;
            CoreGameSignals.Instance.onReset -= OnRestart;
        }

        private void OnDisable()
        {
            UnsbscribeEvents();
        }

        private void OnRestart()
        {
            if (!this.gameObject.activeInHierarchy)
                gameObject.SetActive(true);
        }

        #endregion
    }
}
