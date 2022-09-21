using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody move;
        
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        [Header("Data")] private PlayerData _playerData;

        private float _inputSpeed;

        private Vector2 _clamp;

        private bool _isTouchingPlayer = true;

        private bool _station = true;

        private float _lerpDelay = 1;

        private float direct;

        #endregion

        #endregion

        private void Start()
        {
            _playerData = GetPlayerData();
            Reset();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<SO_PlayerData>("Data/SO_PlayerData").PlayerData;
        }
        
        public void movementcontroller(HorizontalInputParams inputParams)
        {
            _inputSpeed = inputParams.XValue;
            _clamp = inputParams.ClampValues;
        }

        private void FixedUpdate()
        {
            if (_isTouchingPlayer)
            {
                if (_station)
                    Move();

                else
                {
                    StopMove();
                }

            }
        }
        
        public void ObstacleMove()
        {
            transform.DOMoveZ(transform.position.z - 10, 1);
        }
            
        private void Move()
        {
            move.velocity = new Vector3(_inputSpeed * _playerData.MovementSide, move.velocity.y, _playerData.MoveSpeed);
            move.position = new Vector3(Mathf.Clamp(move.position.x, _clamp.x, _clamp.y), move.position.y, move.position.z);
        }

        private void StopMove()
        {
            move.velocity = Vector3.zero;
        }

        public void Play()
        {
            _isTouchingPlayer = true;
            _station = true;
            animator.SetTrigger("Run");
        }

        public void Finish()
        {
            animator.SetTrigger("Idle");
            _station = false;
        }

        public void Reset()
        {
            _isTouchingPlayer = false;
            _station = false;
        }
    }
}