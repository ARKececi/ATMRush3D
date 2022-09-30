using System;
using Controllers.UIManager;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController panelController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onPlay += OnPlay;
            UISignals.Instance.onNext += OnNext;
            UISignals.Instance.onIncome += OnIncome;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onNext -= OnNext;
            UISignals.Instance.onPlay -= OnPlay;
            UISignals.Instance.onIncome += OnIncome;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Play()
        {
            panelController.OnClosePanel(UIPanel.PlayButton);
            panelController.OnClosePanel(UIPanel.IncomeButton);
            panelController.OnClosePanel(UIPanel.StackButton);
            CoreGameSignals.Instance.onPlay?.Invoke();
            CameraSignals.Instance.onPlayEnter?.Invoke();
        }

        public void Next()
        {
            panelController.OnClosePanel(UIPanel.NextButton);
            CoreGameSignals.Instance.onWinStation?.Invoke();
            ScoreSignals.Instance.onScoreReset?.Invoke();
            DOVirtual.DelayedCall(.1f, () => CameraSignals.Instance.onReset?.Invoke());
            DOVirtual.DelayedCall(.1f, () => StackSignals.Instance.onRestFollow?.Invoke());
            DOVirtual.DelayedCall(1f, () => OnPlay());
            DOVirtual.DelayedCall(1f, () => OnIncome());
            DOVirtual.DelayedCall(1f, () => OnStack());
            DOVirtual.DelayedCall(1f,()=>CoreGameSignals.Instance.onMiniGameReset?.Invoke());
            CoreGameSignals.Instance.onResetShop?.Invoke();
            SaveSignals.Instance.onSave?.Invoke();
        }

        public void Income()
        {
            CoreGameSignals.Instance.onIncome?.Invoke();
        }

        public void Stack()
        {
            CoreGameSignals.Instance.onStack?.Invoke();
        }

        private void OnIncome()
        {
            panelController.OnOpenPanel(UIPanel.IncomeButton);
        }

        private void OnStack()
        {
            panelController.OnOpenPanel(UIPanel.StackButton);
        }
        public void OnPlay()
        {
            panelController.OnOpenPanel(UIPanel.PlayButton);
        }

        private void OnNext()
        {
            panelController.OnOpenPanel(UIPanel.NextButton);
        }
    }
}