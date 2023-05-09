using GridRunner.PoolModule.Interfaces;
using GridRunner.PoolModule.Signals;
using GridRunner.PoolModule.Enums;
using UnityEngine;
using System.Collections.Generic;

public class GridSquareBackground : MonoBehaviour, IReleasePoolObject
{
    public AvaibleType AvaibleType = AvaibleType.Unlock;
    public GameObject guru;
    public List<GridSquareBackground> MyNeighbors = new List<GridSquareBackground>();

    private void OnEnable()
    {
        AvaibleType = AvaibleType.Unlock;
        MyNeighbors.Clear();
        MyNeighbors.TrimExcess();
        if (this.transform.childCount > 0)
            ReleaseObject(this.transform.GetChild(0).gameObject);
    }

    public void GetNeighbors(GridSquareBackground gameObject)
    {
        MyNeighbors.Add(gameObject);
    }

    public void ReleaseObject(GameObject obj)
    {
        guru.SetActive(false);
        //PoolSignals.Instance.onReleaseObjectFromPool(obj, poolType);
    }

    public void ReleaseObject(GameObject obj, PoolType poolType)
    {
        guru.SetActive(false);
        PoolSignals.Instance.onReleaseObjectFromPool(obj, poolType);
    }
}

public enum AvaibleType
{
    Lock,
    Unlock,
}
