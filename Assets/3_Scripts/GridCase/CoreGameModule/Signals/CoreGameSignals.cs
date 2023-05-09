using UnityEngine.Events;
using GridRunner.Enums;

namespace GridRunner.Grid.CoreGameModule.Signals
{
    public class CoreGameSignals : AbstractSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameState = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction<int> onUpdateGridGameScore = delegate { };
    }
}
