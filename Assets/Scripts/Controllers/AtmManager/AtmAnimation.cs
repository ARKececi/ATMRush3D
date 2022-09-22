using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class AtmAnimation : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

        private Vector3 atmVector;

        #endregion

        #endregion

        public void AtmReceiveAnimation(GameObject other)
        {
            atmVector = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.position.z);
            other.transform.DOScale(new Vector3(0, 0, 0), 1);
            other.transform.DOMove(atmVector, 0.5f);
        }
    }
}