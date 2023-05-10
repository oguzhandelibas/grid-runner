using GridRunner.PoolModule.Interfaces;
using GridRunner.PoolModule.Signals;
using GridRunner.PoolModule.Enums;
using GridRunner.Grid.GridModule.Signals;
using GridRunner.InputModule.Signals;
using UnityEngine;

namespace GridRunner.Grid.GridModule
{
    public class GridManager : MonoBehaviour, IGetPoolObject, IReleasePoolObject
    {
        [SerializeField]
        private GridData GridData;
        [SerializeField]
        private Transform gridPivotTarget;

        private GridSquareBackground[,] _gridArray;

        private readonly Vector2[] neighborsPattern =
        {
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1)
        };

        private Vector3 _gridPositions;
        private float gridPivotCalculate;
        private Camera _camera;
        private GridClickCommand _gridClickCommand;

        private void Start()
        {
            _gridClickCommand = new GridClickCommand(GetGridMatchParticle(), GetGridClickParticle());
            _camera = Camera.main;
        }

        private ParticleSystem GetGridMatchParticle() => GetObject(PoolType.GridMatchParticle).GetComponent<ParticleSystem>();
        private ParticleSystem GetGridClickParticle() => GetObject(PoolType.GridClickParticle).GetComponent<ParticleSystem>();

        #region Event Subscriptions

        private void OnEnable()
        {
            SubsciribeEvents();
        }
        private void SubsciribeEvents()
        {
            GridSignals.Instance.onCreateGrid += OnCreateGrid;
            InputSignals.Instance.onClick += OnClick;
        }
        private void UnsubsciribeEvents()
        {
            GridSignals.Instance.onCreateGrid -= OnCreateGrid;
            InputSignals.Instance.onClick -= OnClick;
        }
        private void OnDisable()
        {
            UnsubsciribeEvents();
        }
        #endregion

        private void OnClick()
        {
            _gridClickCommand.Click();
        }

        private void OnCreateGrid(int gridInputSize)
        {
            if (gridInputSize <= 0)
                return;
            if (transform.childCount > 0)
                ReleaseAllGridObject(transform, PoolType.GridObject);

            var gridCount = gridInputSize * gridInputSize;
            _gridArray = new GridSquareBackground[gridInputSize, gridInputSize];
            GridData.GridSize = gridInputSize;

            var gridOffsetsMax = Mathf.Max(GridData.GridOffsets.x, GridData.GridOffsets.y);
            _camera.orthographicSize = (gridInputSize * gridOffsetsMax) + 1;

            gridPivotTarget.localPosition = new Vector3(
                -CheckPivotPosition(gridInputSize) * GridData.GridOffsets.x,
                gridPivotTarget.localPosition.y,
                -CheckPivotPosition(gridInputSize) * GridData.GridOffsets.y);

            var position = gridPivotTarget.position;

            for (int i = 0; i < gridCount; i++)
            {
                var modX = i % gridInputSize;
                var modZ = i / gridInputSize % gridInputSize;
                var objPosition = new Vector3(modX * GridData.GridOffsets.x + position.x, position.y, modZ * GridData.GridOffsets.y + position.z);

                var obj = GetObject(PoolType.GridObject);
                obj.transform.SetParent(transform);
                obj.transform.position = objPosition;

                var objComponent = obj.GetComponent<GridSquareBackground>();
                _gridArray[modX, modZ] = objComponent;
            }

            FoundAllNeighbors(gridCount);
        }

        private void FoundAllNeighbors(int gridCount)
        {
            for (int i = 0; i < gridCount; i++)
            {
                var modX = (int)i % GridData.GridSize;
                var divideZ = (i / GridData.GridSize);
                var currentGrid = _gridArray[modX, divideZ];

                foreach (var neighborPattern in neighborsPattern)
                {
                    var neighborXIndex = modX + neighborPattern.x;
                    var neighborZIndex = divideZ + neighborPattern.y;
                    if (CheckEdges(neighborXIndex, neighborZIndex)) continue;
                    currentGrid.GetNeighbors(_gridArray[(int)neighborXIndex, (int)neighborZIndex]);
                }
            }
        }

        private bool CheckEdges(float neighborX, float neighborZ)
        {
            return (neighborX >= GridData.GridSize || neighborX < 0 || neighborZ >= GridData.GridSize || neighborZ < 0);
        }

        private float CheckPivotPosition(int gridSize)
        {
            return (gridSize - 1) * 0.5f;
        }

        private void ReleaseAllGridObject(Transform transformParent, PoolType poolType)
        {
            for (int i = transformParent.childCount - 1; i >= 0; i--)
            {
                ReleaseObject(transformParent.GetChild(i).gameObject, poolType);
            }
        }

        public GameObject GetObject(PoolType poolType)
        {
            return PoolSignals.Instance.onGetObjectFromPool(poolType);
        }

        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(obj, poolType);
        }
    }
}



