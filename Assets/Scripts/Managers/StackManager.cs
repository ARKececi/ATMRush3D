using System.Collections.Generic;
using Controllers;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackAddController stackAddController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onStackAdd += OnStackAdd;
            StackSignals.Instance.onStackDistributing += OnStackDistributing;
            StackSignals.Instance.onRemoveList += OnRemoveList;
            StackSignals.Instance.onList += OnList;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onStackAdd -= OnStackAdd;
            StackSignals.Instance.onStackDistributing -= OnStackDistributing;
            StackSignals.Instance.onRemoveList -= OnRemoveList;
            StackSignals.Instance.onList -= OnList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnStackAdd(StackObjectParams other)
        {
            
            stackAddController.ObjectController(other.other);
            stackAddController.StackAddObject(other.other, stackAddController._objects.Count-1);
        }

        public void OnStackDistributing(StackObjectParams other)
        {
            stackAddController.StackDistributing(other.other);
        }
        
        private void OnRemoveList(int i)
        {
            stackAddController.RemoveList(i);
        }

        private List<GameObject> OnList()
        {
            return stackAddController._objects;
        }
    }
}