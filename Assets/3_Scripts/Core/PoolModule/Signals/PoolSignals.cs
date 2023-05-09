using UnityEngine;
using UnityEngine.Events;
using GridRunner.PoolModule.Enums;
using System;

namespace GridRunner.PoolModule.Signals
{
    public class PoolSignals : AbstractSingleton<PoolSignals>
    {
        public Func<PoolType, GameObject> onGetObjectFromPool = delegate { return null; };
        public UnityAction<GameObject, PoolType> onReleaseObjectFromPool = delegate { };
    }
}
