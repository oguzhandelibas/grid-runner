using System.Xml;
using UnityEngine;
using GridRunner.PoolModule.Interfaces;
using GridRunner.PoolModule.Signals;
using GridRunner.PoolModule.Enums;
using GridRunner.Runner.LevelModule.Signals;
using System.Collections.Generic;
using GridRunner.Runner.CutModule.Data;
using GridRunner.Runner.CutModule.Data.ScriptableObjects;
using GridRunner.Runner.CoreGameModule.Signals;
using GridRunner.Runner.CutModule.Controllers;
using GridRunner.Runner.CutModule.Commands;
using GridRunner.InputModule.Signals;

namespace GridRunner.Runner.CutModule
{
    public class StackCubeSpawnerManager : MonoBehaviour, IGetPoolObject, IReleasePoolObject
    {
        #region Serializable Variables

        public int Count;

        #region Serializable Variables
        [SerializeField]
        private List<GameObject> _stackCubes;

        [SerializeField]
        private GameObject finishObject;

        [SerializeField]
        private StackCubeCutController stackCubeCutController;

        [SerializeField]
        private StackCubeOnClickController stackCubeOnClickController;
        #endregion

        #region Private Variables

        private CalculateFinishCommand calculateFinishCommand;

        private float _stackCubeOffsetZ = 0;
        private int _maxCubeCount;
        private bool _isFailed = false;
        private StackCubeData _stackCubeData;

        #endregion

        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _stackCubeData = GetData().StackCubeData;
            var lastMaxCubeCount = _maxCubeCount;
            _maxCubeCount = GetData().StackCountsEachLevel[LevelSignals.Instance.onGetLevel.Invoke()];
            calculateFinishCommand = new CalculateFinishCommand(finishObject);
        }

        private CD_StackCube GetData()
        {
            return Resources.Load<CD_StackCube>("Datas/CD_StackCube");
        }
        private void Start()
        {
            OnClick();
        }

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onClick += OnClick;
            LevelSignals.Instance.onRestartLevel += OnRestart;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        private void UnsbscribeEvents()
        {
            InputSignals.Instance.onClick -= OnClick;
            LevelSignals.Instance.onRestartLevel -= OnRestart;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnsbscribeEvents();
        }
        #endregion

        private void OnClick()
        {
            if (_isFailed) return;

            var lastCube = _stackCubes.Count > 1 ? _stackCubes[_stackCubes.Count - 1] : null;
            var firstCube = _stackCubes[0];
            var lastCubeRenderer = lastCube?.GetComponentInChildren<MeshRenderer>();

            if (Count >= _maxCubeCount)
            {
                if (finishObject.activeInHierarchy)
                    calculateFinishCommand.GetFinishObject(_stackCubes);
                return;
            }

            if (lastCube != null)
            {
                lastCube.transform.localScale = lastCube.transform.localScale;
                stackCubeCutController.CutObject(_stackCubes);
            }

            stackCubeOnClickController.Click(_stackCubes, _stackCubeOffsetZ, _maxCubeCount, _stackCubeData);
        }

        private void OnLevelFailed() => _isFailed = true;

        private void OnLevelSuccessful()
        {
            if (_stackCubes.Count == 0) return;

            var firstCube = _stackCubes[0];
            var lastCubeRenderer = _stackCubes[_stackCubes.Count - 1].GetComponentInChildren<MeshRenderer>();

            firstCube.transform.position = new Vector3(firstCube.transform.position.x,
                                                        firstCube.transform.position.y,
                                                        finishObject.transform.position.z + ((finishObject.transform.localScale.z + firstCube.transform.localScale.z) / 2));

            firstCube.GetComponentInChildren<MeshRenderer>().material.color = lastCubeRenderer.material.color;
        }



        #region Reset and Restart Jobs
        private void ResetSpawner()
        {
            for (int i = 1; i < _stackCubes.Count; i++)
            {
                ReleaseObject(_stackCubes[i], PoolType.MovementStackCube);
            }

            _isFailed = false;
            _stackCubeOffsetZ = 0;
            var obj = _stackCubes[0];
            _stackCubes.Clear();
            _stackCubes.Add(obj);
        }

        private void OnRestart()
        {
            OnClick();
            ResetSpawner();
            Count = 0;

        }
        private void OnReset()
        {
            Count = 0;
            ResetSpawner();
            OnClick();
            Init();
        }
        #endregion

        public GameObject GetObject(PoolType poolType)
        {
            return PoolSignals.Instance.onGetObjectFromPool(poolType);
        }
        public void ReleaseObject(GameObject obj, PoolType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool(obj, poolType);
        }
        public float GetCutEdge()
        {
            return _stackCubes[_stackCubes.Count - 1].transform.position.x - _stackCubes[_stackCubes.Count - 2].transform.position.x;
        }
    }

}