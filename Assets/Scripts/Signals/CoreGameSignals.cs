using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onObstacleMove = delegate { };
        
        public UnityAction<StackObjectParams> onMoneyCount = delegate { };
        
        public UnityAction onAtmMove = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };
        
        public UnityAction onFinish = delegate { };
        
        public UnityAction<int> onSetPlayerScore = delegate { };
        
        public UnityAction<int> onSetScore = delegate {  };
    }
}