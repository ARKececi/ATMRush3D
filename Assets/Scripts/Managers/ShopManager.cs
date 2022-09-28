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

        [SerializeField] private TextMeshProUGUI incomeText;

        [SerializeField] private TextMeshProUGUI stackText;

        #endregion

        #region Private Variables

        private int _incomeCount;

        private int _value;

        private int _stackCount;

        private int _stackValue;

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
            CoreGameSignals.Instance.onResetShop += OnResetIncome;
            CoreGameSignals.Instance.onBuy += OnBuy;
            CoreGameSignals.Instance.onStack += OnStack;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onIncome -= OnIncome;
            CoreGameSignals.Instance.onResetShop += OnResetIncome;
            CoreGameSignals.Instance.onBuy -= OnBuy;
            CoreGameSignals.Instance.onStack -= OnStack;
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
            _value = 200;
            _buy = false;
        }

        private void Start()
        {
            OnResetIncome();
            stackText.text = "Stack\n" + _value * (_stackCount);
        }

        private void OnResetIncome()
        {
            incomeText.text = "Income\n" + _value * (_incomeCount + 1);
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
            return Resources.Load<SO_ShopData>("Data/SO_ShopData").shopdata; // saveden çekilecek 
        }

        private void OnBuy(bool buy)
        {
            _buy = buy;
        }

        public void OnIncome()
        {
            if (_incomeCount < 3)
            {
                ScoreSignals.Instance.onShopScoreCalculation?.Invoke(_value * (_incomeCount + 1));
                if (_buy != false)
                {
                    _incomeCount += 1;
                    GetShopData().Income = _incomeCount;
                    DOVirtual.DelayedCall(.2f, () => IncomeInstantiate());
                    if (_incomeCount < 3)
                    {
                        incomeText.text = "Income\n" + _value * (_incomeCount + 1);   
                    }
                    else incomeText.text = "Income\n" + "Max";
                    
                    
                }
            }
            else incomeText.text = "Income\n" + "Max";
        }

        private void IncomeInstantiate()
        {
            var obje = Instantiate(money);
            StackSignals.Instance.onStackAdd(new StackObjectParams()
                {
                    other = obje
                });
        }

        private void OnStack()
        {
            if (_stackCount < 4)
            {
                ScoreSignals.Instance.onShopScoreCalculation?.Invoke(_value * (_stackCount));
                if (_buy != false)
                {
                    Debug.Log("burdayım");
                    _stackCount += 1;
                    GetShopData().Stack = _stackCount;
                    ScoreSignals.Instance.onScoreXValue?.Invoke(_stackCount);
                    if (_stackCount < 4)
                    {
                        stackText.text = "Stack\n" + _value * (_stackCount);   
                    }
                    else stackText.text = "Stack\n" + "Max";
                    
                    
                }
            }
            else stackText.text = "Stack\n" + "Max";
            
        }
    }
}