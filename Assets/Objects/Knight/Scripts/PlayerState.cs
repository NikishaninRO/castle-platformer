using UnityEngine;

namespace Knight
{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public MovementState CurrentState { get; private set; } = MovementState.Idle;

        public void SetCurrentState(MovementState newState)
        {
            CurrentState = newState;
        }

        public bool IsGroundedState()
        {
            return CurrentState == MovementState.Idle || CurrentState == MovementState.Moving || CurrentState == MovementState.Sprinting;
        }
    }

    public enum MovementState
    {
        Idle = 0,
        Moving = 1,
        Sprinting = 2,
        Jumping = 3,
        Falling = 4,
    }
}
