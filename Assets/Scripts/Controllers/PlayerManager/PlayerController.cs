using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro score;

        #endregion

        #endregion

        public void SetScore(int i)
        {
            score.text = i.ToString();
        }
    }
}