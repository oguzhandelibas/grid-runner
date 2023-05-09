using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using GridRunner.PoolModule.Enums;

namespace GridRunner.PoolModule.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "GridRunner/Datas/CD_Pool", order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public List<PoolType> poolTypes;
        public List<PoolData> poolDatas;
        public int Count { get => poolDatas.Count; }
        //[SerializeField] public SerializedDictionary<PoolType, PoolData> PoolDataDic = new SerializedDictionary<PoolType, PoolData>();
    }
}
