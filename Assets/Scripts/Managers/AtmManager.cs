using Controllers;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AtmController atmController;
        
        [SerializeField] private TextMeshPro atmText;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onMoneyCount += OnMoneyCount;
            CoreGameSignals.Instance.onSetScore += OnSetScore;
            CoreGameSignals.Instance.onMoneyVariableCount += OnMoneyVariableCount;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onMoneyCount -= OnMoneyCount;
            CoreGameSignals.Instance.onSetScore -= OnSetScore;
            CoreGameSignals.Instance.onMoneyVariableCount += OnMoneyVariableCount;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnMoneyCount(StackObjectParams other)
        {
            atmController.MoneyVariableCount(other.other);
        }

        private void OnSetScore(int score)
        {
            atmText.text = score.ToString();
        }

        private void OnMoneyVariableCount(GameObject other)
        {
            atmController.MoneyVariableCount(other);
        }

    }
}