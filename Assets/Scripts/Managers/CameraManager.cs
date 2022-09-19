﻿using System;
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

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.onEnterFinisStation += OnEnterFinisStation;
            CameraSignals.Instance.onSetCamera += OnSetCamera;
        }

        private void UnsubscribeEvents()
        {
            CameraSignals.Instance.onEnterFinisStation -= OnEnterFinisStation;
            CameraSignals.Instance.onSetCamera -= OnSetCamera;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
        private void OnEnterFinisStation()
        {
            switch (_cameraState)
            {
                case CameraState.Runner:
                    _cameraState = CameraState.Init; 
                    animator.SetTrigger(_cameraState.ToString());
                    break;
            
            
                case CameraState.Init:
                    _cameraState = CameraState.Runner; 
                    animator.SetTrigger(_cameraState.ToString());
                    break;
            }
            
        }

        private void Start()
        {
            player = levelHolder.transform.GetChild(0).transform.GetChild(0).gameObject;
            OnSetCamera();
        }

        private void OnSetCamera()
        {
            vmStateCamera.Follow = player.transform;
        }
    }
}