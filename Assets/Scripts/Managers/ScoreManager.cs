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
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void ScoreCalculation(GameObject other)
        {
                MoneyName = other.GetComponentInChildren<MeshFilter>();
                switch (MoneyName.mesh.name)
                {
                    case "Money Instance" :
                        _score = 1;
                        break;
                    case "gold Instance":
                        _score = 2;
                        break;
                    case "diamond Instance":
                        _score = 4;
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
            ScoreCalculation(other);
            _atmScore += _score;
            CoreGameSignals.Instance.onSetScore?.Invoke(_atmScore);
        }

        private void OnSetScore()
        {
            CoreGameSignals.Instance.onGetScore?.Invoke(_playerScore);
        }

        private void OnShowScore()
        {
            _mainScore += _playerScore;
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

        private void OnScoreReset()
        {
            _playerScore = 0;
            _atmScore = 0;
        }
        
    }
}