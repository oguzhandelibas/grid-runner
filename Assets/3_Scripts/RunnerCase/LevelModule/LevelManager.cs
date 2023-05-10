using UnityEngine;
using GridRunner.Runner.CoreGameModule.Signals;
using GridRunner.Runner.LevelModule.Signals;
using GridRunner.Runner.LevelModule.Data;
using GridRunner.Runner.LevelModule.Data.ScriptableObjects;
using GridRunner.Runner.LevelModule.Commands;
using GridRunner.Runner.CutModule.Data;
using GridRunner.Runner.CutModule.Data.ScriptableObjects;

namespace GridRunner.Runner.LevelModule
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [Header("Data")]
        [SerializeField]
        private LevelData levelData;

        [SerializeField]
        private GameObject levelHolder;

        #endregion

        #region Private Variables

        private ClearActiveLevelCommand _clearActiveLevel;
        private LevelLoaderCommand _levelLoader;

        private int _levelID;
        private int _levelIDForText;
        private int _uniqeID = 1234;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            InitLevelData();
        }
        private void InitLevelData()
        {
            levelData = GetLevelData();
            _levelID = levelData.LevelID;
            _levelIDForText = levelData.LevelIDForText;
            /*if (!ES3.FileExists(this.levelData.GetKey().ToString() + $"{_uniqeID}.es3")) // _levelId asl�nda uniqeid
            {
                if (!ES3.KeyExists(this.levelData.GetKey().ToString()))
                {
                    levelData = GetLevelData();
                    _levelIDForText = levelData.LevelIDForText;
                    _levelID = levelData.LevelID;
                    Save(_uniqeID);
                }
            }*/
            Load(_uniqeID);
        }

        private void Init()
        {
            Debug.Log("Init()s");
            _levelLoader = new LevelLoaderCommand(ref levelHolder);
            _clearActiveLevel = new ClearActiveLevelCommand(ref levelHolder);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            //InitializeDataSignals.Instance.onLoadLevelID += OnLoadLevelID;
            LevelSignals.Instance.onLevelInitialize += OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            LevelSignals.Instance.onRestartLevel += OnRestartLevel;
            LevelSignals.Instance.onGetLevel += GetLevelCount;
            LevelSignals.Instance.onGetLevelForText += GetLevelCountForText;
        }

        private void UnsubscribeEvents()
        {
            //InitializeDataSignals.Instance.onLoadLevelID -= OnLoadLevelID;
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
            LevelSignals.Instance.onGetLevel -= GetLevelCount;
            LevelSignals.Instance.onGetLevelForText -= GetLevelCountForText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private int GetLevelCount()
        {
            return _levelID;
        }
        private int GetLevelCountForText()
        {
            return _levelIDForText;
        }
        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Datas/CD_Level").LevelData;
        }
        private int GetStackCountData()
        {
            return Resources.Load<CD_StackCube>("Datas/CD_StackCube").StackCountsEachLevel.Count;
        }

        private void OnInitializeLevel()
        {
            _levelLoader.Execute();
            LevelSignals.Instance.onLevelInitDone?.Invoke();
        }

        private void OnClearActiveLevel()
        {
            _clearActiveLevel.Execute();
        }

        private void OnNextLevel()
        {
            _levelID++;
            _levelIDForText++;
            if (_levelID >= GetStackCountData())
                _levelID = 0;
            Save(_uniqeID);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void OnRestartLevel()
        {
            Debug.Log("Restart Level");
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
        }
        public void Load(int uniqeID)
        {
            Debug.Log("Load System Currently Unavailable");
            /*
            LevelData _data = SaveLoadSignals.Instance.onLoadLevelData?.Invoke(SaveLoadType.LevelData, uniqeID);
            levelData = _data;
            _levelID = levelData.LevelID;
            _levelIDForText = levelData.LevelIDForText;*/
        }

        public void Save(int uniqeID)
        {
            Debug.Log("Load System Currently Unavailable");
            /*
            LevelData newlevelData = new LevelData(_levelID, _levelIDForText);
            SaveLoadSignals.Instance.onSaveLevelData?.Invoke(newlevelData, uniqeID);*/
        }
    }
}
