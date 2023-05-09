using UnityEngine.Events;
using GridRunner.Grid.UIModule.Enums;

namespace GridRunner.Grid.UIModule.Signals
{
    public class UISignals : AbstractSingleton<UISignals>
    {
        public UnityAction<PanelTypes> onOpenPanel = delegate { };
        public UnityAction<PanelTypes> onClosePanel = delegate { };
        public UnityAction<int> onUpdateGridScoreText = delegate { };
    }
}
