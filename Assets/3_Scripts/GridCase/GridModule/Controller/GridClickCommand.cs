using GridRunner.PoolModule.Signals;
using GridRunner.PoolModule.Interfaces;
using GridRunner.PoolModule.Enums;

using GridRunner.Grid.UIModule.Signals;
using GridRunner.Grid.CoreGameModule.Signals;

using UnityEngine;
using System.Collections.Generic;

public class GridClickCommand : IGetPoolObject, IReleasePoolObject
{
    private readonly int _scoreRaiseAmount = 1;
    private List<GridSquareBackground> _neighbors = new List<GridSquareBackground>();
    private ParticleSystem _matchParticle;
    private ParticleSystem _clickParticle;
    public GridClickCommand(ParticleSystem matchParticle, ParticleSystem clickParticle)
    {
        _matchParticle = matchParticle;
        _clickParticle = clickParticle;
    }
    public void Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out GridSquareBackground gridSquareManager))
            {
                if (gridSquareManager.AvaibleType != AvaibleType.Lock)
                {
                    gridSquareManager.guru.SetActive(true);
                    gridSquareManager.AvaibleType = AvaibleType.Lock;
                    _neighbors.Clear();
                    CheckNeighbors(gridSquareManager);

                    if (_neighbors.Count >= 3)
                    {
                        CoreGameSignals.Instance.onUpdateGridGameScore?.Invoke(_scoreRaiseAmount);
                        UISignals.Instance.onUpdateGridScoreText?.Invoke(1);
                        _matchParticle.gameObject.SetActive(true);
                        _matchParticle.transform.position = gridSquareManager.transform.position;
                        _matchParticle.Play();

                        for (int i = _neighbors.Count - 1; i >= 0; i--)
                        {
                            //ReleaseObject(_neighbors[i].transform.GetChild(0).gameObject, PoolType.GridCrownObject);
                            _neighbors[i].AvaibleType = AvaibleType.Unlock;
                            _neighbors[i].guru.SetActive(false);

                        }
                        _neighbors.Clear();
                    }
                    else
                    {
                        _clickParticle.transform.position = gridSquareManager.transform.position;
                        _clickParticle.gameObject.SetActive(true);
                        _clickParticle.Play();
                    }
                }
            }
        }
    }

    private void CheckNeighbors(GridSquareBackground gridSquareManager)
    {
        if (_neighbors.Contains(gridSquareManager)) return;
        _neighbors.Add(gridSquareManager);
        for (int i = 0; i < gridSquareManager.MyNeighbors.Count; i++)
            if (gridSquareManager.MyNeighbors[i].AvaibleType == AvaibleType.Lock)
                CheckNeighbors(gridSquareManager.MyNeighbors[i]);
    }

    public GameObject GetObject(PoolType poolType)
    {
        return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolType);
    }

    public void ReleaseObject(GameObject obj, PoolType poolType)
    {
        PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(obj, poolType);
    }
}
