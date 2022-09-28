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

        [SerializeField] private Material WallMetarial;

        #endregion

        private float _scoreX;

        private GameObject _scoreObject;

        private GameObject _fakeMoney;

        private int _score;
        
        private float _changesColor;

        private Vector3 _fakePos;

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
            CoreGameSignals.Instance.onMiniGameReset += OnMiniGameReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onFakePlayer -= OnFakePlayer;
            CoreGameSignals.Instance.onGetScore -= OnGetScore;
            CoreGameSignals.Instance.onMiniGameReset -= OnMiniGameReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            _fakePos = transform.GetChild(0).localPosition;
            _scoreX = 1.0f;
        }

        private void Start()
        {
            DOVirtual.DelayedCall(.2f, WallInstantiate);
            DOVirtual.DelayedCall(.2f, FakeMoney);
        }

        private void OnGetScore(int score)
        {
            _score = score;
        }

        private void WallInstantiate()
        {
            for (int i = 0; i < 100; i++)
            {
                _scoreObject = Instantiate(wall, transform.GetChild(1));
                _scoreObject.transform.localPosition = new Vector3(0, i * 5,0);
                WallScoreIncrease();
            }
        }

        private void WallScoreIncrease()
        {
            _scoreObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "X" +(_scoreX);
            _scoreX += 0.1f;
        }

        private void OnFakePlayer()
        {
            fakePlayer.SetActive(true);
            CameraSignals.Instance.onFakeState?.Invoke();
            StartCoroutine(MovingPlayer());
        }

        private void FakeMoney()
        {
            for (int i = 0; i <= 20; i++)
            {
                _fakeMoney = Instantiate(fakeMoney, transform.GetChild(0));
                _fakeMoney.transform.localPosition = new Vector3(0, -1 * i, -10);
            }
        }
        
        IEnumerator MovingPlayer()
        {
            ScoreSignals.Instance.onSetScore?.Invoke();
            transform.GetChild(0).DOMoveY(Mathf.Clamp(_score, 0, 900), 3f).SetEase(Ease.Flash).SetDelay(1);
            Debug.Log(_score);
            yield return new WaitForSeconds(4.5f);
            ScoreSignals.Instance.onShowScore?.Invoke();
            UISignals.Instance.onNext?.Invoke();
        }

        public void WallShow(Collider other)
        {
            _changesColor = (0.036f + _changesColor) % 1;
            other.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor, 1, 1), 0.1f);
            other.transform.DOLocalMoveZ(-2, .2f).SetDelay(.1f);
        }

        private void OnMiniGameReset()
        {
            for (int i = 0; i < 100; i++)
            {
                transform.GetChild(1).GetChild(i).GetComponent<Renderer>().material = WallMetarial;
            }
            fakePlayer.SetActive(false);
            fakePlayer.transform.localPosition = _fakePos;
        }
        
        
    }
}