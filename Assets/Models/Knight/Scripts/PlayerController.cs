using UnityEngine;

namespace Knight
{
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GameObject _followPoint;
        [Header("Movement")]
        public float RunAcceleration = 30f;
        public float MaxRunSpeed = 4f;
        public float Drag = 0.1f;
        [Header("Sprint")]
        public float SprintAcceleration = 40f;
        public float MaxSprintSpeed = 8f;
        [Header("Look")]
        public float LookSenseH = 0.1f;
        public float LookSenseV = 0.1f;
        private float LookLimitV = 45f;
        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerState _playerState;

        private Vector2 _cameraRotation = Vector2.zero;
        private Vector2 _playerTargetRotation = Vector2.zero;

        private void Start()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
            HandleHorizontalMovement();
            UpdatetState();
        }

        private void LateUpdate()
        {
            HandleRotation();
        }

        private void HandleHorizontalMovement()
        {
            bool isSprinting = _playerState.CurrentState == MovementState.Sprinting;
            float currentAcceleration = isSprinting ? SprintAcceleration : RunAcceleration;
            float currentMaxSpeed = isSprinting ? MaxSprintSpeed : MaxRunSpeed;
            Vector3 directionForwardXZ = new Vector3(_characterController.transform.forward.x, 0f, _characterController.transform.forward.z).normalized;
            Vector3 directionRightXZ = new Vector3(_characterController.transform.right.x, 0f, _characterController.transform.right.z).normalized;
            Vector3 movementDirection = directionRightXZ * _playerLocomotionInput.MovementInput.x + directionForwardXZ * _playerLocomotionInput.MovementInput.y;
            Vector3 movementDelta = movementDirection * currentAcceleration * Time.deltaTime;
            Vector3 newVelocity = _characterController.velocity + movementDelta;
            Vector3 currentDrag = newVelocity.normalized * Drag * Time.deltaTime;
            newVelocity = newVelocity.magnitude > Drag * Time.deltaTime ? newVelocity - currentDrag : Vector3.zero;
            newVelocity = Vector3.ClampMagnitude(newVelocity, currentMaxSpeed);
            _characterController.Move(newVelocity * Time.deltaTime);
        }

        private void HandleRotation()
        {
            _cameraRotation.x += LookSenseH * _playerLocomotionInput.LookInput.x;
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y + LookSenseV * _playerLocomotionInput.LookInput.y, -LookLimitV, LookLimitV);
            _followPoint.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
            _playerTargetRotation.x += transform.eulerAngles.x + LookSenseH * _playerLocomotionInput.LookInput.x;
            transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);
        }

        private void UpdatetState()
        {
            bool isZeroMovementInput = _playerLocomotionInput.MovementInput == Vector2.zero;
            bool isForwardMovement = _playerLocomotionInput.MovementInput.y == 1 && _playerLocomotionInput.MovementInput.x == 0;
            bool isSprinting = _playerLocomotionInput.SprintToggledOn && !isZeroMovementInput && isForwardMovement;
            MovementState newState = MovementState.Idle;
            if (!isZeroMovementInput)
            {
                newState = MovementState.Moving;
            }
            if (isSprinting)
            {
                newState = MovementState.Sprinting;
            }
            _playerState.SetCurrentState(newState);
        }
    }
}
