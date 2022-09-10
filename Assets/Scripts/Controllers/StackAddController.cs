using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
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
        
        [Header("Data")] private StackData _stackData;

        private Vector3 _newPos;

        private Vector3 _stackPos;

        private float _moveDelay;

        private float direct;

        #endregion

        #region Public Variables

        public List<GameObject> _objects;

        #endregion

        #endregion

        private void Awake()
        {
            _stackData = GetPlayerData();
            
        }
        
        private StackData GetPlayerData()
        {
            return Resources.Load<SO_StackData>("Data/SO_StackData").StackData;
        }

        private void Update()
        {
            MoveStackObject();
                _objects[0].transform.localPosition = new Vector3(player.transform.localPosition.x,0.1f,0.8f) ;
        }

        public void ObjectController(GameObject other)
        {
            if (!_objects.Contains(other.gameObject))
            {
                other.GetComponentInChildren<BoxCollider>().isTrigger = false;
                other.transform.GetChild(1).gameObject.tag = "Collected";
            }
        }

        public void StackAddObject(GameObject other, int index)
        {
            if (!_objects.Contains(other.gameObject))
            {
                other.transform.parent = collected.transform;
                _newPos = _objects[index].transform.localPosition;
                _newPos.z += 0.5f;
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
                 direct = Mathf.Lerp(_objects[i].transform.localPosition.x, _stackPos.x, _stackData.LerpDelay);
                _objects[i].transform.localPosition = new Vector3(direct, _stackPos.y, _stackPos.z);
            }
        }

        /*private void MoveOrigin()
        {
            for (int i = 1; i < _objects.Count; i++)
            {
                _stackPos = _objects[i].transform.localPosition;
                _stackPos.x = _objects[0].transform.localPosition.x;
                _objects[i].transform.DOLocalMove(_stackPos,  _moveDelay);
            }
        }*/
    }
}