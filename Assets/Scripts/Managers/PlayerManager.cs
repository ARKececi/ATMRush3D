using Controllers;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        [SerializeField] private TextMeshPro score;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnMovement;
            CoreGameSignals.Instance.onObstacleMove += OnObstacleMove;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onSetPlayerScore += OnSetScore;

        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnMovement;
            CoreGameSignals.Instance.onObstacleMove -= OnObstacleMove;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onSetPlayerScore -= OnSetScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void OnMovement(HorizontalInputParams horizontalInputParams)
        {
            playerMovementController.movementcontroller(horizontalInputParams);
        }

        private void OnObstacleMove()
        {
            playerMovementController.ObstacleMove();
        }

        public void OnSetScore(int i)
        {
            score.text = i.ToString();
        }

        private void OnReset()
        {
            playerMovementController.Reset();
        }

        private void OnPlay()
        {
            playerMovementController.Play();
        }

        public void OnFinish()
        {
            playerMovementController.Finish();
        }
        
    }
}