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

        public void StackDistributingAnimation(int index, List<GameObject> objects, Vector3 distributPos)
        {
            objects[index].GetComponentInChildren<BoxCollider>().isTrigger = false;
            //objects[index].transform.DOLocalMove(distributPos, 0.1f);
            objects[index].transform.DOLocalJump(distributPos, 2f,3,0.5f);
        }

    }
}