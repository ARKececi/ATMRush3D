using Signals;
using UnityEngine;

namespace Controllers.ScoreManager
{
    public class ScoreController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private MeshFilter MoneyName;
        
        private int Score;
        
        #endregion

        #endregion

        public void MoneyVariable(GameObject Object)
        {
            MoneyName = Object.GetComponentInChildren<MeshFilter>();
            switch (MoneyName.mesh.name)
            {
                case "Money Instance" :
                    Score += 10;
                    CoreGameSignals.Instance.onSetScore?.Invoke(Score);
                    break;
                case "gold Instance":
                    Score += 20;
                    CoreGameSignals.Instance.onSetScore?.Invoke(Score);
                    break;
                case "diamond Instance":
                    Score += 40;
                    CoreGameSignals.Instance.onSetScore?.Invoke(Score);
                    break;
            }
        }
    }
}