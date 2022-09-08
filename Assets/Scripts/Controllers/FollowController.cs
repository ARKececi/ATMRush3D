using System;
using UnityEngine;

namespace Controllers
{
    public class FollowController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject player;

        #endregion

        #endregion

        private void Update()
        {
            transform.localPosition = new Vector3(0 , player.transform.position.y, player.transform.position.z);
        }
    }
    
}