﻿using Controllers;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AtmController atmController;

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

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onMoneyCount -= OnMoneyCount;
            CoreGameSignals.Instance.onSetScore -= OnSetScore;
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

        private void OnSetScore(int Score)
        {
            atmController.SetScore(Score);
        }
        
    }
}