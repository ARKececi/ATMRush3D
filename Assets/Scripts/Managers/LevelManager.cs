using Data.UnityObject;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _levelCount;

        private int _levelID;

        #endregion

        #endregion
        
        private void Awake()
        {
            _levelCount = GetActiveLevel();
            Debug.Log(_levelCount);
        }
        
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }
        
        private void Start()
        {
            WinLevelID();
        }
        
        private void WinLevelID()
        {
            _levelID = _levelCount % Resources.Load<SO_LevelData>("Data/SO_Level")
                .Levels.Count;
        }
    }
}