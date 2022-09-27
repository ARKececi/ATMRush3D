using System;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ShopManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized

        [SerializeField] private GameObject money;

        [SerializeField] private TextMeshProUGUI IncomeText;

        #endregion

        #region Private Variables

        private int _incomeCount;

        private int _incomeValue;

        private int _stackCount;

        private int _stackMoney;

        private bool _buy;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onIncome += OnIncome;
            CoreGameSignals.Instance.onResetShop += OnResetShop;
            CoreGameSignals.Instance.onBuy += OnBuy;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onIncome -= OnIncome;
            CoreGameSignals.Instance.onResetShop += OnResetShop;
            CoreGameSignals.Instance.onBuy -= OnBuy;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            _incomeCount = GetShopData().Income;
            _stackCount = GetShopData().Stack;
            _incomeValue = 200;
            _buy = false;
        }

        private void Start()
        {
            OnResetShop();
        }

        private void OnResetShop()
        {
            IncomeText.text = "Income\n" + _incomeValue * (_incomeCount + 1);
            if (_incomeCount >= 1)
            {
                for (int i = 0; i < _incomeCount; i++)
                {
                    DOVirtual.DelayedCall(.2f, () => IncomeInstantiate());
                }
            }
        }

        private ShopData GetShopData()
        {
            return Resources.Load<SO_ShopData>("Data/SO_ShopData").shopdata;
        }

        private void OnBuy(bool buy)
        {
            _buy = buy;
        }

        public void OnIncome()
        {
            if (_incomeCount < 3)
            {
                ScoreSignals.Instance.onShopScoreCalculation?.Invoke(_incomeValue * (_incomeCount + 1));
                if (_buy != false)
                {
                    _incomeCount += 1;
                    GetShopData().Income = _incomeCount;
                    DOVirtual.DelayedCall(.2f, () => IncomeInstantiate());
                    IncomeText.text = "Income\n" + _incomeValue * (_incomeCount + 1);
                }
            }
        }

        private void IncomeInstantiate()
        {
            var obje = Instantiate(money);
            StackSignals.Instance.onStackAdd(new StackObjectParams()
                {
                    other = obje
                });
        }

        private void Stack()
        {
            if (_stackCount <= 4)
            {
                
            }
        }
    }
}