using System.Collections.Generic;
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

        [SerializeField] private GameObject collected;

        [SerializeField] private TextMeshPro _atmScore;

        #endregion

        #region Private Variables

        private MeshFilter MoneyName;

        private int Score;

        private List<GameObject> _listObjects;

        #endregion

        #endregion

        private int List(GameObject listPoint)
        {
            _listObjects = StackSignals.Instance.onList?.Invoke();
            return _listObjects.IndexOf(listPoint);
        }
        public void MoneyVariable(GameObject other)
        {
         MoneyName = other.GetComponentInChildren<MeshFilter>();
         StackSignals.Instance.onRemoveList?.Invoke(List(other));
         switch (MoneyName.mesh.name)
            {
                case "Money Instance" :
                    Score += 10;
                    SetScore();
                    break;
                case "gold Instance":
                    Score += 20;
                    SetScore();
                    break;
                case "diamond Instance":
                    Score += 40;
                    SetScore();
                    break;
            }
         other.transform.parent = atmAnimation.transform;
         atmAnimation.AtmReceiveAnimation(other);
        }

        private void SetScore()
        {
            _atmScore.text = Score.ToString();
        }
        
    }
}