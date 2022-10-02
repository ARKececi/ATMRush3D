﻿using System;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI scoreText;

        #endregion
        
        #region Private Variables
        
        private MeshFilter MoneyName;
        
        private int _score;

        private int _playerScore;

        private int _atmScore;

        private int _mainScore;

        private int _stackX;

        #endregion
        
        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onPlayerScoreCalculation += OnPlayerScoreCalculation;
            ScoreSignals.Instance.onPlayerScoreDistributing += OnPlayerScoreDistributing;
            ScoreSignals.Instance.onAtmScoreCalculation += OnAtmScoreCalculation;
            ScoreSignals.Instance.onSetScore += OnSetScore;
            ScoreSignals.Instance.onScoreReset += OnScoreReset;
            ScoreSignals.Instance.onShowScore += OnShowScore;
            ScoreSignals.Instance.onShopScoreCalculation += OnShopScoreCalculation;
            ScoreSignals.Instance.onScoreXValue += OnScoreXValue;
            SaveSignals.Instance.onSetMainScore += OnSetMainScore;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onPlayerScoreCalculation -= OnPlayerScoreCalculation;
            ScoreSignals.Instance.onPlayerScoreDistributing -= OnPlayerScoreDistributing;
            ScoreSignals.Instance.onAtmScoreCalculation -= OnAtmScoreCalculation;
            ScoreSignals.Instance.onSetScore -= OnSetScore;
            ScoreSignals.Instance.onScoreReset -= OnScoreReset;
            ScoreSignals.Instance.onShowScore -= OnShowScore;;
            ScoreSignals.Instance.onShopScoreCalculation -= OnShopScoreCalculation;
            ScoreSignals.Instance.onScoreXValue -= OnScoreXValue;
            SaveSignals.Instance.onSetMainScore -= OnSetMainScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            _stackX = GetSaveStack();
            _mainScore = GetSaveScore();
        }

        private void Start()
        {
            _stackX += 1;
            scoreText.text = _mainScore.ToString();
        }

        private int GetSaveStack()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("StackCount") ? ES3.Load<int>("StackCount") : 0;
        }

        private int GetSaveScore()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("MainScore") ? ES3.Load<int>("MainScore") : 0;
        }

        private void OnScoreXValue(int value)
        {
            _stackX = value;
        }

        private void ScoreCalculation(GameObject other)
        {
                MoneyName = other.GetComponentInChildren<MeshFilter>();
                switch (MoneyName.mesh.name)
                {
                    case "Money Instance" :
                        _score = 1 * _stackX;
                        break;
                    case "gold Instance":
                        _score = 2 * _stackX;
                        break;
                    case "diamond Instance":
                        _score = 4 * _stackX;
                        break;
                }
        }

        private void OnPlayerScoreCalculation(GameObject other)
        {
            ScoreCalculation(other);
            _playerScore += + _score;
            CoreGameSignals.Instance.onSetPlayerScore?.Invoke(_playerScore);
        }

        private void OnPlayerScoreDistributing(GameObject other)
        {
            ScoreCalculation(other);
            _playerScore -= _score;
            CoreGameSignals.Instance.onSetPlayerScore?.Invoke(_playerScore);
        }

        private void OnAtmScoreCalculation(GameObject other)
        {
            if (_atmScore <= _playerScore)
            {
                ScoreCalculation(other);
                _atmScore += _score;
                CoreGameSignals.Instance.onSetScore?.Invoke(_atmScore);
            }
        }

        private void OnSetScore()
        {
            CoreGameSignals.Instance.onGetScore?.Invoke(_playerScore);
        }

        private void OnShowScore()
        {
            var Calculation = _playerScore * .275f;
            Calculation = (Calculation * 0.1f);
            _mainScore += Mathf.RoundToInt(Calculation * _playerScore);
            scoreText.text = _mainScore.ToString();
        }

        private void OnShopScoreCalculation(int value)
        {
            if (_mainScore >= value)
            {
                _mainScore -= value;
                scoreText.text = _mainScore.ToString();
                CoreGameSignals.Instance.onBuy?.Invoke(true);
            }

            else
                CoreGameSignals.Instance.onBuy?.Invoke(false);
        }

        private int OnSetMainScore()
        {
            return _mainScore;
        }

        private void OnScoreReset()
        {
            _playerScore = 0;
            _atmScore = 0;
        }
        
    }
}