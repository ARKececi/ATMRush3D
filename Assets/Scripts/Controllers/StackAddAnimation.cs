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

        public void StackAnimationStart()
        {
            StartCoroutine(StackAnimation());
        }
        
        private IEnumerator StackAnimation()
        {
            _objects = _stackAdd._objects;
            for (int i = _objects.Count-1; i > 0; i--)
            {
                _scale = new Vector3(0.5f, 0.5f, 0.5f);
                _scale *= 1.5f;
                _objects[i].transform.DOScale(_scale, 0.1f).OnComplete(() => 
                    _objects[i].transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f));
                yield return new WaitForSeconds(0.05f);


            }
        }
    }
}