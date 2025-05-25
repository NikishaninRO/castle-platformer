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
    }

    public enum MovementState
    {
        Idle = 0,
        Moving = 1,
        Sprinting = 2,
    }
}
