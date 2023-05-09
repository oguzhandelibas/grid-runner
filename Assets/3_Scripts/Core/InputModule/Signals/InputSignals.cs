using UnityEngine.Events;

namespace GridRunner.InputModule.Signals
{

    public class InputSignals : AbstractSingleton<InputSignals>
    {
        public UnityAction onClick = delegate { };
    }
}
