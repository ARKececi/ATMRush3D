using Controllers.UIManager;
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
            UISignals.Instance.onNext += OnNext;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onNext -= OnNext;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnPlay()
        {
            panelController.OnClosePanel(UIPanel.PlayButton);
            CoreGameSignals.Instance.onPlay?.Invoke();
            CameraSignals.Instance.onPlayEnter?.Invoke();
        }

        public void Next()
        {
            panelController.OnClosePanel(UIPanel.NextButton);
        }

        public void Play()
        {
            OnPlay();
        }

        private void OnNext()
        {
            panelController.OnOpenPanel(UIPanel.NextButton);
        }
    }
}