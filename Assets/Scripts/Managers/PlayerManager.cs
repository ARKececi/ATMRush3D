using Controllers;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        [SerializeField] private PlayerController playerController;

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
            //CoreGameSignals.Instance.onSetPlayerScore += OnSetScore;

        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnMovement;
            CoreGameSignals.Instance.onObstacleMove -= OnObstacleMove;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            //CoreGameSignals.Instance.onSetPlayerScore -= OnSetScore;
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

        private void OnSetScore(int score)
        {
            playerController.SetScore(score);
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