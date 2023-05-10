using UnityEngine.Events;
using GridRunner.AudioModule.Enums;

namespace GridRunner.AudioModule.Signals
{
    public class AudioSignals : AbstractSingleton<AudioSignals>
    {
        public UnityAction<SoundType, float> onPlaySound = delegate { };
    }
}
