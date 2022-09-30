using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSave = delegate { };
        public Func<int> onSetMainScore = delegate { return 0; };
    }
}