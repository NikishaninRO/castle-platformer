using UnityEngine;

namespace Knight
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _locomotionBlendSpeed = 0.02f;
        public static int _inputXHash = Animator.StringToHash("InputX");
        public static int _inputYHash = Animator.StringToHash("InputY");
        public static int _isSprintingHash = Animator.StringToHash("IsSprinting");
        public static int _isGroundedgHash = Animator.StringToHash("IsGrounded");
        public static int _isJumpingHash = Animator.StringToHash("IsJumping");
        public static int _isFallingHash = Animator.StringToHash("IsFalling");

        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerState _playerState;
        private Vector3 _currentBlendInput = Vector3.zero;

        private void Start()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
            SetMovementParams();
            SetSprintingParam();
            SetGroundedParam();
            SetJumpingParam();
            SetFallingParam();
        }

        private void SetMovementParams()
        {
            Vector2 movementInput = _playerLocomotionInput.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, movementInput, _locomotionBlendSpeed + Time.deltaTime);
            _animator.SetFloat(_inputXHash, _currentBlendInput.x);
            _animator.SetFloat(_inputYHash, _currentBlendInput.y);
        }

        private void SetSprintingParam()
        {
            bool isSprinting = _playerState.CurrentState == MovementState.Sprinting;
            _animator.SetBool(_isSprintingHash, isSprinting);
        }

        private void SetGroundedParam()
        {
            bool isGrounded = _playerState.IsGroundedState();
            _animator.SetBool(_isGroundedgHash, isGrounded);
        }

        private void SetJumpingParam()
        {
            bool isJumping = _playerState.CurrentState == MovementState.Jumping;
            _animator.SetBool(_isJumpingHash, isJumping);
        }

        private void SetFallingParam()
        {
            bool isFalling = _playerState.CurrentState == MovementState.Falling;
            _animator.SetBool(_isFallingHash, isFalling);
        }
    }
}
