using System;
using System.Collections;
using Cinemachine;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using CameraState = Enums.CameraState;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private CinemachineStateDrivenCamera vmStateCamera;

        [SerializeField] private Animator animator;

        [SerializeField] private GameObject levelHolder;
        
        [SerializeField] private GameObject player;
        
        #endregion

        #region Private Variables

        private CameraState _cameraState;

        private Vector3 _resetPosition;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.onPlayEnter += OnPlayEnter;
            CameraSignals.Instance.onSetCamera += OnSetCamera;
            CameraSignals.Instance.onFakeState += OnFakeState;
            CameraSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CameraSignals.Instance.onPlayEnter -= OnPlayEnter;
            CameraSignals.Instance.onSetCamera -= OnSetCamera;
            CameraSignals.Instance.onFakeState -= OnFakeState;
            CameraSignals.Instance.onReset += OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
        private void OnPlayEnter()
        {
            switch (_cameraState)
            {
                case CameraState.Runner:
                    _cameraState = CameraState.Initialized; 
                    animator.SetTrigger(_cameraState.ToString());
                    break;
            
            
                case CameraState.Initialized:
                    _cameraState = CameraState.Runner; 
                    animator.SetTrigger(_cameraState.ToString());
                    break;
                
            }
            
        }

        private void OnFakeState()
        {
            
            StartCoroutine(FakeState());
            
        }

        IEnumerator FakeState()
        {
            animator.SetTrigger("Fake");
            yield return new WaitForSeconds(1);

        }

        private void Start()
        {
            OnSetCamera();
            _resetPosition = transform.position;
        }

        private void OnSetCamera()
        {
            player = levelHolder.transform.GetChild(0).transform.GetChild(0).gameObject;
            vmStateCamera.Follow = player.transform;
        }

        private void OnReset()
        {
            OnSetCamera();
            OnPlayEnter();
            transform.position = _resetPosition;
        }
    }
}