using System;
using System.Collections.Generic;
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
        public UnityAction<int> onRemoveList = delegate { };
        public Func<List<GameObject>> onList = delegate { return new List<GameObject>(0); };
        public UnityAction<GameObject> onFinish =delegate { };
        public UnityAction<GameObject> onObjectRemoveList = delegate { };
    }
}