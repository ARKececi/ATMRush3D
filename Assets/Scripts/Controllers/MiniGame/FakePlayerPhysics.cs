using DG.Tweening;
using UnityEngine;

namespace Controllers.MiniGame
{
    public class FakePlayerPhysics : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Managers.MiniGame miniGame;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if ( other.CompareTag("Wall"))
            {
                miniGame.WallShow(other);
            }
        }
    }
}