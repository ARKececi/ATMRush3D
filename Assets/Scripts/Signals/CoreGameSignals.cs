using System;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<SaveDataParams> onSaveGame = delegate { };
        public Func<int> onSetActiveLevel = delegate { return 0; };
        public UnityAction onObstacleMove = delegate { };
        public UnityAction<StackObjectParams> onMoneyCount = delegate { };
        public UnityAction onAtmMove = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onFinish = delegate { };
        public UnityAction<int> onSetPlayerScore = delegate { };
        public UnityAction<int> onSetScore = delegate {  };
        public UnityAction onFakePlayer = delegate { };
        public UnityAction<int> onGetScore = delegate { };
        public UnityAction<GameObject> onMoneyVariableCount = delegate { };
        public UnityAction onWinStation = delegate { };
        public UnityAction onMiniGameReset = delegate { };
        public UnityAction onIncome = delegate { };
        public UnityAction onResetShop = delegate { };
        public UnityAction<bool> onBuy = delegate { };
        public UnityAction onStack = delegate { };
        public Func<int> onSetStack = delegate { return 0;};
        public Func<int> onSetIncome = delegate { return 0; };
    }
}