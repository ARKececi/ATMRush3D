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

        [SerializeField] private TextMeshProUGUI stackText;

        [SerializeField] private TextMeshProUGUI incomeText;

        #endregion

        #region Private Variables

        private int _stackCount;

        private int _value;

        private int _incomeCount;

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
            CoreGameSignals.Instance.onResetShop += OnStartStack;
            CoreGameSignals.Instance.onBuy += OnBuy;
            CoreGameSignals.Instance.onStack += OnStack;
            CoreGameSignals.Instance.onSetStack += OnSetStack;
            CoreGameSignals.Instance.onSetIncome += OnSetIncome;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onIncome -= OnIncome;
            CoreGameSignals.Instance.onResetShop -= OnStartStack;
            CoreGameSignals.Instance.onBuy -= OnBuy;
            CoreGameSignals.Instance.onStack -= OnStack;
            CoreGameSignals.Instance.onSetStack -= OnSetStack;
            CoreGameSignals.Instance.onSetIncome -= OnSetIncome;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            _stackCount = GetSaveStack();
            _incomeCount = GetSaveIncome();
            _value = 200;
            _buy = false;
        }

        private int GetSaveIncome()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("IncomeCount") ? ES3.Load<int>("IncomeCount") : 0;
        }
        
        private int GetSaveStack()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("StackCount") ? ES3.Load<int>("StackCount") : 0;
        }

        private void Start()
        {
            OnStartStack();
            OnStartIncome();
        }

        private int OnSetStack()
        {
            return _stackCount;
        }

        private int OnSetIncome()
        {
            return _incomeCount;
        }

        private void OnStartIncome()
        {
            
            if (_incomeCount < 4)
            {
                incomeText.text = "Income\n" + _value * (_incomeCount + 1);   
            }
            else incomeText.text = "Income\n" + "Max";
        }

        private void OnStartStack()
        {
            if (_stackCount < 3)
            {
                stackText.text = "Stack\n" + _value * (_stackCount + 1);   
            }
            else stackText.text = "Stack\n" + "Max";
            if (_stackCount >= 1)
            {
                for (int i = 0; i < _stackCount; i++)
                {
                    DOVirtual.DelayedCall(.2f, () => StackInstantiate());
                }
            }
        }

        private void OnBuy(bool buy)
        {
            _buy = buy;
        }

        public void OnStack()
        {
            if (_stackCount < 3)
            {
                ScoreSignals.Instance.onShopScoreCalculation?.Invoke(_value * (_stackCount + 1));
                if (_buy != false)
                {
                    _stackCount += 1;
                    DOVirtual.DelayedCall(.2f, () => StackInstantiate());
                    if (_stackCount < 3)
                    {
                        stackText.text = "Stack\n" + _value * (_stackCount + 1);   
                    }
                    else stackText.text = "Stack\n" + "Max";
                }
            }
            else stackText.text = "Stack\n" + "Max";
        }

        private void StackInstantiate()
        {
            var obje = Instantiate(money);
            StackSignals.Instance.onStackAdd(new StackObjectParams()
                {
                    other = obje
                });
        }

        private void OnIncome()
        {
            if (_incomeCount < 4)
            {
                ScoreSignals.Instance.onShopScoreCalculation?.Invoke(_value * (_incomeCount + 1));
                if (_buy != false)
                {
                    _incomeCount += 1;
                    ScoreSignals.Instance.onScoreXValue?.Invoke(_incomeCount);
                    if (_incomeCount < 4)
                    {
                        incomeText.text = "Income\n" + _value * (_incomeCount + 1);   
                    }
                    else incomeText.text = "Income\n" + "Max";

                }
            }
            else incomeText.text = "Income\n" + "Max";
            
        }
    }
}