using UnityEngine;

namespace Knight
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _locomotionBlendSpeed = 0.02f;
        public static int _inputXHash = Animator.StringToHash("InputX");
        public static int _inputYHash = Animator.StringToHash("InputY");
        private PlayerLocomotionInput _playerLocomotionInput;
        private Vector3 _currentBlendInput = Vector3.zero;

        void Start()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        }

        void Update()
        {
            Vector2 movementInput = _playerLocomotionInput.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, movementInput, _locomotionBlendSpeed + Time.deltaTime);
            _animator.SetFloat(_inputXHash, _currentBlendInput.x);
            _animator.SetFloat(_inputYHash, _currentBlendInput.y);
        }
    }
}
