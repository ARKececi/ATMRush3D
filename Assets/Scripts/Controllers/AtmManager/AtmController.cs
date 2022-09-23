using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class AtmController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private AtmAnimation atmAnimation;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        public void MoneyVariableCount(GameObject other)
        {
             StackSignals.Instance.onObjectRemoveList?.Invoke(other);
             other.transform.parent = atmAnimation.transform;
             atmAnimation.AtmReceiveAnimation(other);
             ScoreSignals.Instance.onAtmScoreCalculation(other);
             
        }

        public void SetScore(int score)
        {
            
        }

        public void AtmMove()
        {
            transform.DOMoveY(-4, 0.2f);
        }
        
    }
}