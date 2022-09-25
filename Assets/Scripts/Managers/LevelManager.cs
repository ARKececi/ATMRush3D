using System;
using Controllers.LevelManager;
using Data.UnityObject;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        [SerializeField] private LevelLoaderController levelLoader;

        #endregion

        #region Private Variables

        private int _levelCount;

        private int _levelID;

        #endregion

        #endregion
        
        private void Awake()
        {
            _levelID = GetActiveLevel();
            Debug.Log(_levelID);
        }
        
        private int GetActiveLevel()
        {
            return _levelID % Resources.Load<SO_LevelData>("Data/SO_LevelData").Levels.Count;

        }

        private void Start()
        {
            OnLoaderLevel();
        }

        private void OnLoaderLevel()
        {
            levelLoader.LoaderLevel(_levelID, levelHolder.transform);
        }
    }
}