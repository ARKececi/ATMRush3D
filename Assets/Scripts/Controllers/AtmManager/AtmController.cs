using System.Collections.Generic;
using DG.Tweening;
using Keys;
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

        [SerializeField] private TextMeshPro atmScore;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        public void MoneyVariableCount(GameObject other)
        {
             ScoreSignals.Instance.onScoreCalculation(other);
             StackSignals.Instance.onObjectRemoveList?.Invoke(other);
             other.transform.parent = atmAnimation.transform;
             atmAnimation.AtmReceiveAnimation(other);
        }

        public void SetScore(int Score)
        {
            //atmScore.text = Score.ToString();
        }

        public void AtmMove()
        {
            transform.DOMoveY(-4, 0.2f);
        }
        
    }
}