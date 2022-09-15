using UnityEngine;

namespace Controllers
{
    public class AtmPhysics : MonoBehaviour
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
                Debug.Log("burdayım");
                atmController.MoneyVariable(other.transform.parent.gameObject);
            }
            
        }
    }
}