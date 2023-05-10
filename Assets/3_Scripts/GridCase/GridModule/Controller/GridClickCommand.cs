using GridRunner.PoolModule.Signals;
using GridRunner.PoolModule.Interfaces;
using GridRunner.PoolModule.Enums;

using GridRunner.Grid.UIModule.Signals;
using GridRunner.Grid.CoreGameModule.Signals;

using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class GridClickCommand : IGetPoolObject, IReleasePoolObject
{
    private const int ScoreRaiseAmount = 1;
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
        if (!TryGetGridSquareBackground(out var gridSquareBackground)) return;

        if (gridSquareBackground.AvaibleType == AvaibleType.Lock) return;

        ActivateGuruAndLock(gridSquareBackground);

        _neighbors.Clear();
        CheckNeighbors(gridSquareBackground);

        if (_neighbors.Count >= 3)
        {
            RaiseScore();
            ShowMatchParticles(gridSquareBackground);
            ReleaseLockedNeighbors();
        }
        else
        {
            ShowClickParticles(gridSquareBackground);
        }
    }

    private bool TryGetGridSquareBackground(out GridSquareBackground gridSquareBackground)
    {
        gridSquareBackground = null;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit)) return false;

        gridSquareBackground = hit.collider.gameObject.GetComponent<GridSquareBackground>();
        return gridSquareBackground != null;
    }

    private void ActivateGuruAndLock(GridSquareBackground gridSquareBackground)
    {
        gridSquareBackground.guru.SetActive(true);
        gridSquareBackground.AvaibleType = AvaibleType.Lock;
    }

    private void CheckNeighbors(GridSquareBackground gridSquareBackground)
    {
        if (_neighbors.Contains(gridSquareBackground)) return;

        _neighbors.Add(gridSquareBackground);

        foreach (var neighbor in gridSquareBackground.MyNeighbors)
        {
            if (neighbor.AvaibleType == AvaibleType.Lock)
            {
                CheckNeighbors(neighbor);
            }
        }
    }

    private void RaiseScore()
    {
        CoreGameSignals.Instance.onUpdateGridGameScore?.Invoke(ScoreRaiseAmount);
        UISignals.Instance.onUpdateGridScoreText?.Invoke(ScoreRaiseAmount);
    }

    private void ShowMatchParticles(GridSquareBackground gridSquareBackground)
    {
        _matchParticle.gameObject.SetActive(true);
        _matchParticle.transform.position = gridSquareBackground.transform.position;
        _matchParticle.Play();
    }

    private void ReleaseLockedNeighbors()
    {
        for (var i = _neighbors.Count - 1; i >= 0; i--)
        {
            _neighbors[i].AvaibleType = AvaibleType.Unlock;
            _neighbors[i].guru.transform.DOShakePosition(0.5f, .1f, 10, 90);
            GuruActiveness(_neighbors[i]);
        }

        _neighbors.Clear();
    }

    private void GuruActiveness(GridSquareBackground neighbor)
    {
        DOVirtual.DelayedCall(0.5f, () => { neighbor.guru.SetActive(false); });
    }

    private void ShowClickParticles(GridSquareBackground gridSquareBackground)
    {
        _clickParticle.transform.position = gridSquareBackground.transform.position;
        _clickParticle.gameObject.SetActive(true);
        _clickParticle.Play();
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
