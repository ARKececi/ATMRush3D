using System;
using UnityEngine;

namespace Controllers
{
    public class FollowController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        [SerializeField] private GameObject player;
        
        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void Start()
        {
            Follow();
        }

        public void Follow()
        {
            player = levelHolder.transform.GetChild(0).transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            if (player != null)
            {
                transform.position = new Vector3(0 , player.transform.position.y, player.transform.position.z);
            }
            
        }
    }
    
}