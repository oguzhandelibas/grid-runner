using UnityEngine;
using UnityEngine.Rendering;
using GridRunner.PoolModule.Enums;

namespace GridRunner.PoolModule.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "StackGame/CD_Pool", order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public SerializedDictionary<PoolType, PoolData> PoolDataDic = new SerializedDictionary<PoolType, PoolData>();
    }
}
