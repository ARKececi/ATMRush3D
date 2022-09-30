using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSave += OnSave;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSave -= OnSave;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnSave()
        {
            OnSaveGame(new SaveDataParams()
            {
                IncomeCount = CoreGameSignals.Instance.onSetIncome(),
                StackCount = CoreGameSignals.Instance.onSetStack(),
                MainScore = SaveSignals.Instance.onSetMainScore(),
                LevelCount = CoreGameSignals.Instance.onSetActiveLevel()
                
                
            });
        }
        
        private void OnSaveGame(SaveDataParams saveDataParams)
        {
            if (saveDataParams.IncomeCount != null) ES3.Save("IncomeCount", saveDataParams.IncomeCount);
            if (saveDataParams.StackCount != null) ES3.Save("StackCount", saveDataParams.StackCount);
            if (saveDataParams.MainScore != null) ES3.Save("MainScore", saveDataParams.MainScore);
            if (saveDataParams.LevelCount != null) ES3.Save("LevelCount", saveDataParams.LevelCount);
        }
    }
}