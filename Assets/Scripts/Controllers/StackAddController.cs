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
                other.transform.GetChild(1).gameObject.tag = "Collected";
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
            _stackPos.z = _objects[_index].transform.localPosition.z;
            for (int i = _index; i <= _objects.Count - 1; i++)
            {
                _objects[i].transform.GetChild(1).gameObject.tag = "Money";
                _randomStackPosX = Random.Range(4, -4);
                _randomStackPosZ = Random.Range(10, 20);
                _objects[i].transform.localPosition = new Vector3(_randomStackPosX, _stackPos.y, _stackPos.z + _randomStackPosZ);
                _objects[i].transform.parent = Collected.transform;
            }

            for (int i = _objects.Count - 1; i >= _index; i--)
            {
                _objects.Remove(_objects[i]);
                _objects.TrimExcess();
            }
            
        }
        
        
    }
}