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
    }
}