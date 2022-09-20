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

        private void OnPlay()
        {
            panelController.OnClosePanel(UIPanel.PlayButton);
            CoreGameSignals.Instance.onPlay?.Invoke();
            CameraSignals.Instance.onPlayEnter?.Invoke();
        }

        public void Play()
        {
            OnPlay();
        }
    }
}