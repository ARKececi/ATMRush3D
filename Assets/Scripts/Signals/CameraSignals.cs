using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction onEnterFinisStation = delegate { };
        public UnityAction onSetCamera = delegate { };
    }
}