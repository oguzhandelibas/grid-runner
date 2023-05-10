using UnityEngine;
using GridRunner.PoolModule.Signals;
using GridRunner.PoolModule.Interfaces;
using GridRunner.PoolModule.Enums;
//using DG.Tweening;

public class CuttedCubes : MonoBehaviour, IReleasePoolObject
{
    private const float _time = 1f;
    public void ReleaseObject(GameObject obj, PoolType poolType)
    {
        PoolSignals.Instance.onReleaseObjectFromPool(obj, poolType);
    }

    public void ReleaseObject(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        //DOVirtual.DelayedCall(_time, ()=>  ReleaseObject(this.gameObject, PoolType.CuttedCubes));
    }
}
