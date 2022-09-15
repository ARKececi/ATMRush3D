using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class StackAddController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackAddAnimation stackAnimation;

        [SerializeField] private GameObject stack;

        [SerializeField] private GameObject Collected;
        
        [SerializeField] private GameObject player;

        [SerializeField] private StackObstacleAnimation stackObstacleAnimation;

        #endregion

        #region Private Variables
        
        [Header("Data")] private StackData _stackData;

        private Vector3 _newPos;

        private Vector3 _stackPos;

        private float _moveDelay;

        private float direct;

        private int _index;

        private float _randomStackPosX;

        private float _randomStackPosZ;

        private Vector3 _distribut;

        #endregion

        #region Public Variables
        
        public Vector3 _distributingPos;

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
                other.transform.GetChild(1).gameObject.tag = "Collected";
                other.GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
        }

        public void StackAddObject(GameObject other, int index)
        {
            if (!_objects.Contains(other.gameObject))
            {
                other.transform.parent = stack.transform;
                _newPos = _objects[index].transform.localPosition;
                _newPos.z += 1;
                other.transform.localPosition = _newPos;
                _objects.Add(other);
                stackAnimation.StackAnimationStart();
            }
        }

        private void MoveStackObject()
        {
            for (int i = 1; i < _objects.Count; i++)
            {
                _stackPos = _objects[i].transform.localPosition;
                _stackPos.x = _objects[i - 1].transform.localPosition.x;
                 direct = Mathf.Lerp(_objects[i].transform.localPosition.x, _stackPos.x, _stackData.LerpDelay);
                _objects[i].transform.localPosition = new Vector3(direct, _stackPos.y, _stackPos.z);
            }
        }

        public void StackDistributing(GameObject other)
        {
            _index = _objects.IndexOf(other);
            _distribut.z = _objects[_index].transform.parent.gameObject.transform.position.z + _index;
            for (int i = _index; i <= _objects.Count - 1; i++)
            {
                _objects[i].transform.GetChild(1).gameObject.tag = "Money";
                _randomStackPosX = Random.Range(4, -4);
                _randomStackPosZ = Random.Range(10, 20);
                _distributingPos = new Vector3(_randomStackPosX, 0.5f, _distribut.z + _randomStackPosZ);
                stackObstacleAnimation.StackDistributingAnimation(i, _objects, _distributingPos);
                //_objects[i].transform.localPosition = _distributingPos;
                _objects[i].transform.parent = Collected.transform;
            }
            
            for (int i = _objects.Count - 1; i >= _index; i--)
            {
                RemoveList(i);
            }
        }

        public void RemoveList(int i)
        {

                _objects.Remove(_objects[i]);
                _objects.TrimExcess();
            
        }
        
        
    }
}