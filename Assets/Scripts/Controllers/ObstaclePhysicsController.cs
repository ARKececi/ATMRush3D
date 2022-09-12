using System;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class ObstaclePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackManager stackManager;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                stackManager.OnStackDistributing(new StackObjectParams()
                {
                    other = other.gameObject.transform.parent.gameObject
                });
                
            }
            
        }
    }
}