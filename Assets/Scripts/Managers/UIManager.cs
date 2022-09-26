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
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onNext -= OnNext;
            UISignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Play()
        {
            panelController.OnClosePanel(UIPanel.PlayButton);
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
            DOVirtual.DelayedCall(1f, () => UISignals.Instance.onPlay?.Invoke());
            DOVirtual.DelayedCall(1f,()=>CoreGameSignals.Instance.onMiniGameReset?.Invoke());
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