using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<StackObjectParams>onStackAdd = delegate { };
        public UnityAction<StackObjectParams> onStackDistributing = delegate { };
    }
}