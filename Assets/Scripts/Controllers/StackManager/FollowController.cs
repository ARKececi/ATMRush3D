using System;
using UnityEngine;

namespace Controllers
{
    public class FollowController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
        
        #endregion

        #region Private Variables

        private GameObject player;

        #endregion

        #endregion

        private void Start()
        {
            player = levelHolder.transform.GetChild(0).transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            transform.position = new Vector3(0 , player.transform.position.y, player.transform.position.z);
        }
    }
    
}