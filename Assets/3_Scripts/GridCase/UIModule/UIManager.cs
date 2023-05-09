using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using GridRunner.Grid.UIModule.Enums;
using GridRunner.Grid.UIModule.Signals;
using System.Collections.Generic;
using GridRunner.Grid.UIModule.Commands;
using GridRunner.Grid.CoreGameModule.Signals;
using GridRunner.Grid.GridModule.Signals;


namespace GridRunner.Grid.UIModule
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;
        [SerializeField] private TextMeshProUGUI gridScoreText;
        [SerializeField] private TextMeshProUGUI sliderValue;
        [SerializeField] private Slider gridSizeSlider;


        #endregion

        #region Private Variables

        private UIPanelCommand _uiPanelCommand;
        private LevelPanelCommand _levelPanelCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _uiPanelCommand = new UIPanelCommand(panels);
            _levelPanelCommand = new LevelPanelCommand(gridScoreText);
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onUpdateGridScoreText += OnUpdateGridScore;

            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;

        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onUpdateGridScoreText -= OnUpdateGridScore;

            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void OnOpenPanel(PanelTypes panelParam)
        {
            _uiPanelCommand.OpenPanel(panelParam);
        }

        private void OnClosePanel(PanelTypes panelParam)
        {
            _uiPanelCommand.ClosePanel(panelParam);
        }

        private void InitPanels()
        {
            _uiPanelCommand.CloseAllPanel();
            _uiPanelCommand.OpenPanel(PanelTypes.GamePanel);
        }

        private void OnReset()
        {
            _uiPanelCommand.CloseAllPanel();
            _uiPanelCommand.OpenPanel(PanelTypes.GamePanel);
        }

        private void OnPlay()
        {
            _uiPanelCommand.CloseAllPanel();
            _uiPanelCommand.OpenPanel(PanelTypes.GamePanel);
        }

        private void OnUpdateGridScore(int coinValue)
        {
            _levelPanelCommand.SetGridScoreText(coinValue);
        }

        #region UI Interact
        public void _UpdateSliderValueOnText()
        {
            sliderValue.text = gridSizeSlider.value.ToString();
        }

        public void _PlayButton()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void _CreatGridButton()
        {
            sliderValue.text = gridSizeSlider.value.ToString();
            var value = (int)gridSizeSlider.value;
            GridSignals.Instance.onCreateGrid?.Invoke(value);
        }

        public void _RestartButton()
        {
            _uiPanelCommand.CloseAllPanel();
            _uiPanelCommand.OpenPanel(PanelTypes.GamePanel);
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
        #endregion





    }
}
