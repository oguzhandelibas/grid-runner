using UnityEngine.Events;
using System;

namespace GridRunner.Runner.LevelModule.Signals
{
    public class LevelSignals : AbstractSingleton<LevelSignals>
    {
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onLevelInitDone = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };

        public Func<int> onGetLevel = delegate { return 0; };
        public Func<int> onGetLevelForText = delegate { return 0; };
    }
}
