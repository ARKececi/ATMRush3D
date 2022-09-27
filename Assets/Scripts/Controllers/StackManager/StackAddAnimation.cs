using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class StackAddAnimation : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackAddController _stackAdd;

        #endregion

        #region Private Variables

        private Vector3 _scale;

        private List<GameObject> _objects;

        #endregion

        #endregion

        private void Awake()
        {
            _objects = _stackAdd._objects;
        }

        public void StackAnimationStart()
        {
            StartCoroutine(StackAnimation());
        }
        
        private IEnumerator StackAnimation()
        {
            for (int i = 0; i <= _objects.Count - 1; i++)
            {
                int index = (_objects.Count - 1) - i;
                _scale = new Vector3(1, 1, 1);
                _scale *= 1.5f;
                
                _objects[index].transform.DOScale(_scale, 0.1f);
                _objects[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f).SetDelay(0.1f);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}