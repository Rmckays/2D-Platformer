using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace States
{
    public class IdleState : State
    {
        [FormerlySerializedAs("MoveState")] public State moveState;
        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(AnimationType.idle);
        }

        protected override void HandleMovement(Vector2 input)
        {
            if (Math.Abs(input.x) > 0)
            {
                agent.TransitionToState(moveState, this);
            }
        }
    }
}
