using UnityEngine;
using GridRunner.PoolModule.Enums;

namespace GridRunner.PoolModule.Interfaces
{
    public interface IGetPoolObject
    {
        GameObject GetObject(PoolType poolType);
    }
}
