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

        [SerializeField] private FollowController followController;

        #endregion

        #region Private Variables

        private int _index;

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
            StackSignals.Instance.onFinish += OnFinish;
            StackSignals.Instance.onObjectRemoveList += OnObjectRemoveList;
            StackSignals.Instance.onListController += OnListController;
            StackSignals.Instance.onRestFollow += OnRest;

        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onStackAdd -= OnStackAdd;
            StackSignals.Instance.onStackDistributing -= OnStackDistributing;
            StackSignals.Instance.onRemoveList -= OnRemoveList;
            StackSignals.Instance.onList -= OnList;
            StackSignals.Instance.onFinish -= OnFinish;
            StackSignals.Instance.onObjectRemoveList -= OnObjectRemoveList;
            StackSignals.Instance.onListController -= OnListController;
            StackSignals.Instance.onRestFollow -= OnRest;
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnStackAdd(StackObjectParams other)
        {
            _index = stackAddController._objects.Count - 1;
            stackAddController.ObjectController(other.other);
            stackAddController.StackAddObject(other.other, _index);
        }

        public void OnStackDistributing(StackObjectParams other)
        {
            stackAddController.StackDistributing(other.other);
        }
        
        private void OnRemoveList(int i)
        {
            stackAddController.RemoveList(i);
        }

        private void OnObjectRemoveList(GameObject obje)
        {
            stackAddController.ObjectRemoveList(obje);
        }

        private List<GameObject> OnList()
        {
            return stackAddController._objects;
        }

        private void OnFinish(GameObject other)
        {
            stackAddController.Finish(other);
        }

        private void OnListController(GameObject other)
        {
            stackAddController.ListController(other);
        }

        private void OnRest()
        {
            followController.Follow();
            stackAddController.Reset();
        }
    }
}