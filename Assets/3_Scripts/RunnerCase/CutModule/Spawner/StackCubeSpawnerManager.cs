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
        //FİNİSHDEN SONRA START CUBE FINISH IN SONUNA GELİYOR, SAKİN OLSUN
        private void OnClick()
        {
            if (_isFailed)
                return;

            if (_stackCubes.Count > 1)
            {
                _stackCubes[_stackCubes.Count - 1].transform.localScale = _stackCubes[_stackCubes.Count - 2].transform.localScale;
                stackCubeCutController.CutObject(_stackCubes);
            }
            if (Count >= _maxCubeCount)
            {
                if (finishObject.activeInHierarchy)
                    calculateFinishCommand.GetFinishObject(_stackCubes);
                return;
            }
            stackCubeOnClickController.Click(_stackCubes, _stackCubeOffsetZ, _maxCubeCount, _stackCubeData);
        }
        private void OnLevelFailed() => _isFailed = true;
        private void OnLevelSuccessful()
        {
            _stackCubes[0].transform.position = new Vector3(_stackCubes[0].transform.position.x,
                _stackCubes[0].transform.position.y,
                finishObject.transform.position.z + ((finishObject.transform.localScale.z + _stackCubes[0].transform.localScale.z) / 2));

            _stackCubes[0].GetComponentInChildren<MeshRenderer>().material.color = _stackCubes[_stackCubes.Count - 1].GetComponentInChildren<MeshRenderer>().material.color;
        }

        #region Reset and Restart Jobs
        private void ResetSpawner()
        {
            for (int i = 1; i < _stackCubes.Count; i++)
            {
                ReleaseObject(_stackCubes[i], PoolType.MovementStackCube);
            }

            var obj = _stackCubes[0];
            _stackCubes.Clear();
            _stackCubes.TrimExcess();
            _stackCubes.Add(obj);

            _isFailed = false;
            _stackCubeOffsetZ = 0;
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

        public void ReleaseObject(GameObject obj)
        {
            throw new System.NotImplementedException();
        }
    }

}