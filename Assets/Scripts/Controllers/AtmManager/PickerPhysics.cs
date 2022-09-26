using Signals;
using UnityEngine;

namespace Controllers
{
    public class PickerPhysics : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AtmController atmController;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Collected"))
            {
                //StackSignals.Instance.onListController?.Invoke(other.transform.parent.gameObject);
                atmController.MoneyVariableCount(other.transform.parent.gameObject);
            }
            
        }
    }
}