using System;
using Keys;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Controllers
{
    public class PlayerPyhsicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private int _score;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                StackSignals.Instance.onStackAdd?.Invoke(new StackObjectParams()
                {
                    other = other.gameObject.transform.parent.gameObject
                });
            }

            if (other.CompareTag("Obstacle"))
            {
                CoreGameSignals.Instance.onObstacleMove?.Invoke();
            }

            if (other.CompareTag("FinishPlayer"))
            {
                playerManager.OnFinish();
            }
            
        }
    }
}