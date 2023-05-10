using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GridRunner.Runner.UIModules.Enums;

namespace GridRunner.Runner.UIModules.Signals
{
    public class UISignals : AbstractSingleton<UISignals>
    {
        public UnityAction<PanelTypes> onOpenPanel = delegate { };
        public UnityAction<PanelTypes> onClosePanel = delegate { };
        public UnityAction<int> onUpdateCoinScoreText = delegate { };
        public UnityAction<int> onUpdateGemScoreText = delegate { };
        public UnityAction<int> onUpdateStarScoreText = delegate { };

    }
}
