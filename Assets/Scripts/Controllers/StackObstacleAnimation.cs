using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class StackObstacleAnimation : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackAddController _stackAdd;

        #endregion

        #region Private Variables

        private Vector3 _distributPos;

        private List<GameObject> _objects;

        #endregion

        #endregion

        public void StackDistributingAnimation(GameObject objects, Vector3 distributPos)
        {
            objects.GetComponentInChildren<BoxCollider>().isTrigger = false;
            objects.transform.DOLocalJump(distributPos, 2f, 1, 0.2f)
                .OnComplete(()=> {objects.GetComponentInChildren<MoneyPhysicsController>().gameObject.tag = "Money";});
        }

    }
}