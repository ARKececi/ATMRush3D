using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onNext = delegate { };
    }
}