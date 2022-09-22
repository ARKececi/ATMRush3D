using Controllers.ScoreManager;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ScoreController scoreController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreCalculation += OnScoreCalculation;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreCalculation -= OnScoreCalculation;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion

        private void OnScoreCalculation(GameObject other)
        {
            scoreController.MoneyVariable(other);
        }
    }
}