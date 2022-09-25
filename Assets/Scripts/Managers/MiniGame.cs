using System;
using System.Collections;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MiniGame : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject wall;

        [SerializeField] private GameObject fakePlayer;

        [SerializeField] private GameObject fakeMoney;

        #endregion

        private float _scoreX;

        private GameObject _scoreObject;

        private GameObject _fakeMoney;

        private int _score;
        
        private float _changesColor;

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onFakePlayer += OnFakePlayer;
            CoreGameSignals.Instance.onGetScore += OnGetScore;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onFakePlayer -= OnFakePlayer;
            CoreGameSignals.Instance.onGetScore -= OnGetScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            _scoreX = 1.0f;
            WallInstantiate();
        }
        
        private void OnGetScore(int score)
        {
            _score = score;
        }

        private void WallInstantiate()
        {
            for (int i = 0; i < 18; i++)
            {
                _scoreObject = Instantiate(wall, transform.GetChild(1));
                _scoreObject.transform.localPosition = new Vector3(0, i * 5,0);
                WallScoreIncrease();
            }
        }

        private void WallScoreIncrease()
        {
            _scoreObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "x" +_scoreX;
            _scoreX += 0.1f;
        }

        private void OnFakePlayer()
        {
            fakePlayer.SetActive(true);
            FakeMoney();
            CameraSignals.Instance.onFakeState?.Invoke();
        }

        private void FakeMoney()
        {
            for (int i = 0; i <= 20; i++)
            {
                _fakeMoney = Instantiate(fakeMoney, transform.GetChild(0));
                _fakeMoney.transform.localPosition = new Vector3(0, -1 * i, -10);
            }

            StartCoroutine(MovingPlayer());
        }
        
        IEnumerator MovingPlayer()
        {
            ScoreSignals.Instance.onSetScore?.Invoke();
            transform.GetChild(0).DOMoveY(Mathf.Clamp(_score, 0, 85), 3f).SetEase(Ease.Flash).SetDelay(1);
            yield return new WaitForSeconds(4.5f);
            UISignals.Instance.onNext?.Invoke();
        }

        public void WallShow(Collider other)
        {
            _changesColor = (0.036f + _changesColor) % 1;
            other.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor, 1, 1), 0.1f);
            other.transform.DOLocalMoveZ(-2, .5f).SetDelay(.5f);
        }
        
        
    }
}