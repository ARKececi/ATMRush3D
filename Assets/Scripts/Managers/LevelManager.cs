using System;
using Controllers.LevelManager;
using Data.UnityObject;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        [SerializeField] private LevelLoaderController levelLoader;

        [SerializeField] private ClearlevelController clearlevel;

        #endregion

        #region Private Variables

        private int _levelCount;

        private int _levelID;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onWinStation += OnWinStation;
            CoreGameSignals.Instance.onSetActiveLevel += OnSetActiveLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onWinStation -= OnWinStation;
            CoreGameSignals.Instance.onSetActiveLevel -= OnSetActiveLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Awake()
        {
            _levelID = GetActiveLevel();
            _levelCount = GetActiveLevel();
            Debug.Log(_levelID);
        }
        
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("LevelCount") ? ES3.Load<int>("LevelCount") : 0;
            //return _levelID % Resources.Load<SO_LevelData>("Data/SO_LevelData").Levels.Count;
        }

        private int OnSetActiveLevel()
        {
            return _levelID;
        }

        private void Start()
        {
            OnLoaderLevel();
        }
        
        private void NextLevelID()
        {
            _levelID = _levelCount % Resources.Load<SO_LevelData>("Data/SO_LevelData")
                .Levels.Count;
            OnClearLevel();
            OnLoaderLevel();
            Debug.Log(_levelID);
        }
        
        private void OnLoaderLevel()
        {
            levelLoader.LoaderLevel(_levelID, levelHolder.transform);
        }
        
        private void OnClearLevel()
        {
            clearlevel.ClearLevel(levelHolder.transform);
        }
        
        private void OnWinStation()
        {
            _levelCount += 1;
            Debug.Log(_levelCount);
            NextLevelID();
        }
    }
}