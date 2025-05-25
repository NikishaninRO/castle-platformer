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
            SetSprintingParams();
        }

        private void SetMovementParams()
        {
            Vector2 movementInput = _playerLocomotionInput.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, movementInput, _locomotionBlendSpeed + Time.deltaTime);
            _animator.SetFloat(_inputXHash, _currentBlendInput.x);
            _animator.SetFloat(_inputYHash, _currentBlendInput.y);
        }

        private void SetSprintingParams()
        {
            bool isSprinting = _playerState.CurrentState == MovementState.Sprinting;
            _animator.SetBool(_isSprintingHash, isSprinting);
        }
    }
}
