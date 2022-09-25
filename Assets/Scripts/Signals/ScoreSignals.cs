using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<GameObject> onPlayerScoreCalculation = delegate { };
        public UnityAction<GameObject> onPlayerScoreDistributing = delegate { };
        public UnityAction<GameObject> onAtmScoreCalculation = delegate { };
        public UnityAction onSetScore = delegate { };
    }
}