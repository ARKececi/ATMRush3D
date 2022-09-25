using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction onPlayEnter = delegate { };
        public UnityAction onSetCamera = delegate { };
        public UnityAction onFakeState = delegate { };
    }
}