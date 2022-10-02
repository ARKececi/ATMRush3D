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

        [SerializeField] private GameObject Collected;
        
        [SerializeField] private GameObject player;

        [SerializeField] private StackObstacleAnimation stackObstacleAnimation;

        [SerializeField] private GameObject levelHolder;

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

        private int _beforeIndex;

        private GameObject _zeroPositon;

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

        private void Start()
        {
            player = levelHolder.transform.GetChild(0).transform.GetChild(0).gameObject;
            Collected = levelHolder.transform.GetChild(0).transform.GetChild(1).gameObject;
            _zeroPositon = _objects[0];
        }

        public void Reset()
        {
            player = levelHolder.transform.GetChild(0).transform.GetChild(0).gameObject;
            Collected = levelHolder.transform.GetChild(0).transform.GetChild(1).gameObject;
            _zeroPositon = _objects[0];
        }

        private StackData GetPlayerData()
        {
            return Resources.Load<SO_StackData>("Data/SO_StackData").StackData;
        }

        private void Update()
        {
            MoveStackObject();
            if (player != null)
            {
                _zeroPositon.transform.localPosition = new Vector3(player.transform.localPosition.x,0.1f,0.8f) ;
            }
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
                if (!(_beforeIndex + 1 >= index))
                {
                    index = _beforeIndex + 1;
                }
                other.transform.parent = transform;
                _newPos = _objects[0].transform.localPosition;
                _newPos.z += index + 0.5f;
                _newPos.x = _objects[index].transform.localPosition.x;
                other.transform.localPosition = _newPos;
                _beforeIndex = index;
                _objects.Add(other);
                ScoreSignals.Instance.onPlayerScoreCalculation?.Invoke(other);
                stackAnimation.StackAnimationStart();
            }
        }

        private void MoveStackObject()
        {
            int value = _objects.Count;
            for (int i = 1; i < value; i++)
            {
                _stackPos = _objects[i].transform.localPosition;
                _stackPos.x = _objects[i - 1].transform.localPosition.x;
                 direct = Mathf.Lerp(_objects[i].transform.localPosition.x, _stackPos.x, _stackData.LerpDelay);
                _objects[i].transform.localPosition = new Vector3(direct, _stackPos.y, _stackPos.z);
            }
        }

        public void StackDistributing(GameObject other)
        {
            int value = _objects.Count;
            _index = _objects.IndexOf(other);

            if (_index != -1)
            {
                _distribut.z = _objects[_index].transform.position.z;
                for (int i = value - 1 ; i >= _index; i--)
                {
                    RandomVector();
                    _distributingPos = new Vector3(_randomStackPosX, 0.5f, _distribut.z + _randomStackPosZ);
                    var obje = _objects[i];
                    stackObstacleAnimation.StackDistributingAnimation(obje, _distributingPos);
                    _objects[i].transform.parent = Collected.transform;
                    ScoreSignals.Instance.onPlayerScoreDistributing?.Invoke(_objects[i]);
                    RemoveList(i);
                    if (i == _index)
                    {
                        other.SetActive(false);
                    }
                    
                }
            }
        }

        public void RandomVector()
        {
            _randomStackPosX = Random.Range(4, -4);
            _randomStackPosZ = Random.Range(5, 15);
        }

        public void RemoveList(int i)
        {
            if (i != -1)
            {
                _objects.Remove(_objects[i]);
                _objects.TrimExcess();
            }
        }

        public void ObjectRemoveList(GameObject other)
        {
            var ObjeId = _objects.IndexOf(other);
            RemoveList(ObjeId);
        }

        public void Finish(GameObject other)
        {
            if (_objects.IndexOf(other) != -1)
            {
                var index = _objects.IndexOf(other);
                int value = _objects.Count;
                if (_objects[index] == null)
                {
                    _objects[index - 1].transform.parent = Collected.transform;
                    _objects[index - 1].transform.DOMoveX(-7, .5f);
                    if (index != -1)
                    {
                        RemoveList(index - 1);
                    }
                }
                else
                {
                    var finisPosition = other.transform.position;
                    for (int i = value -1; i >= index; i--)
                    {
                        _objects[i].transform.position = new Vector3(finisPosition.x, finisPosition.y, finisPosition.z);
                        _objects[i].transform.parent = Collected.transform;
                        _objects[i].transform.DOMoveX(-7, .5f);
                        RemoveList(i);
                    }
                }
            }
        }

        public GameObject ListController(GameObject other)
        {
            var cantains = _objects.IndexOf(other);
            var listCount = _objects.Count;
            if (_objects[cantains] != null)
            {
                for (int i = listCount -1; i >= cantains; i--)
                {
                    return _objects[i];
                }
            }
            else
            {
                return other;
            }

            return null;
        }
    }
}