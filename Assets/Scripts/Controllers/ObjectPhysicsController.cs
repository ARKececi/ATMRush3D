﻿using System;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class ObjectPhysicsController : MonoBehaviour
    {
        #region Self Variables
        #region Serializefield Variables
        [SerializeField] private CollectableManager manager;
        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                StackSignals.Instance.onStackAdd?.Invoke(new StackObjectParams()
                {
                    other = other.gameObject.transform.parent.gameObject
                });
            }

            if (other.CompareTag(("UpdateTrigger")))
            {
                manager.MeshUpdater();
            }
        }
    }
}