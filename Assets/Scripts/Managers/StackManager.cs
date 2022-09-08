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
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onStackAdd -= OnStackAdd;
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
    }
}