using System.Collections.Generic;
using UnityEngine;
using GridRunner.Runner.UIModules.Enums;
using System;

namespace GridRunner.Runner.UIModules.Controllers
{
    public class UIPanelCommands
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _panels;

        #endregion

        #endregion

        public UIPanelCommands(List<GameObject> panels)
        {
            _panels = panels;
        }

        public void OpenPanel(PanelTypes panelParam)
        {
            _panels[(int)panelParam].SetActive(true);
        }

        public void ClosePanel(PanelTypes panelParam)
        {
            _panels[(int)panelParam].SetActive(false);
        }

        public void CloseAllPanel()
        {
            var panelCount = Enum.GetNames(typeof(PanelTypes)).Length;
            for (int i = 0; i < panelCount; i++)
                _panels[i].SetActive(false);
        }
    }
}
