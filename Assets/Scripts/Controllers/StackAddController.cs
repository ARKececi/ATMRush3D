using System;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class StackAddController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackAddAnimation stackAnimation;

        [SerializeField] private GameObject collected;

        [SerializeField] private GameObject player;

        #endregion

        #region Private Variables

        private Vector3 _newPos;

        private Vector3 _stackPos;

        private float _moveDelay;

        #endregion

        #region Public Variables

        public List<GameObject> _objects;

        #endregion

        #endregion

        private void Awake()
        {
            _moveDelay = 0.60f;
        }

        private void Update()
        {
            MoveStackObject();
                _objects[0].transform.localPosition = new Vector3(player.transform.localPosition.x,0.5f,0) ;
        }

        public void ObjectController(GameObject other)
        {
            if (!_objects.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Collected";
                other.AddComponent<ObjectPhysicsController>();
                other.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        public void StackAddObject(GameObject other, int index)
        {
            if (!_objects.Contains(other.gameObject))
            {
                other.transform.parent = collected.transform;
                _newPos = _objects[index].transform.localPosition;
                _newPos.z += 1;
                other.transform.localPosition = _newPos;
                _objects.Add(other);
                stackAnimation.StackAnimationStart();
            }
        }

        public void MoveStackObject()
        {
            for (int i = 1; i < _objects.Count; i++)
            {
                _stackPos = _objects[i].transform.localPosition;
                _stackPos.x = _objects[i - 1].transform.localPosition.x;
                _objects[i].transform.DOLocalMove(_stackPos, _moveDelay);
            }
        }

        private void MoveOrigin()
        {
            for (int i = 1; i < _objects.Count; i++)
            {
                _stackPos = _objects[i].transform.localPosition;
                _stackPos.x = _objects[0].transform.localPosition.x;
                _objects[i].transform.DOLocalMove(_stackPos,  _moveDelay);
            }
        }
    }
}