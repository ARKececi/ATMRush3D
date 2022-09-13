using Keys;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class AtmController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private StackAddController stackAddController;

        [SerializeField] private AtmAnimation atmAnimation;

        [SerializeField] private GameObject collected;

        #endregion

        #region Private Variables

        private MeshFilter MoneyName;

        private int Score;

        #endregion

        #endregion

        public void MoneyVariable(GameObject other)
        {
         MoneyName = other.GetComponentInChildren<MeshFilter>();
         StackSignals.Instance.onRemoveList?.Invoke(stackAddController._objects.IndexOf(other));
         switch (MoneyName.mesh.name)
            {
                case "Money Instance" :
                    Score += 10;
                    break;
                case "gold Instance":
                    Score += 20;
                    break;
                case "diamond Instance":
                    Score += 40;
                    break;
            }
         other.transform.parent = collected.transform;
         atmAnimation.AtmReceiveAnimation(other);
         Debug.Log(Score);
         
        }
    }
}