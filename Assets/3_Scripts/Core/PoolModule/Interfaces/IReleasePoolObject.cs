using UnityEngine;
using GridRunner.PoolModule.Enums;

namespace GridRunner.PoolModule.Interfaces
{
    public interface IReleasePoolObject
    {
        void ReleaseObject(GameObject obj, PoolType poolType);
    }
}
