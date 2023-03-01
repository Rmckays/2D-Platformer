using System;
using UnityEngine;

namespace States
{
    public class MovementState : State
    {
        [SerializeField]
        protected MovementData movementData;

        public State IdleState;

        public float acceleration, deacceleration, maxSpeed;

        protected void Awake()
        {
            movementData = GetComponentInParent<MovementData>();
        }

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(AnimationType.run);
            movementData.horizontalMovementDirection = 0;
            movementData.currentSpeed = 0;
            movementData.currentVelocity = Vector2.zero;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            CalculateVelocity();
            SetPlayerVelocity();
            if (Math.Abs(agent.rigidBody2D.velocity.x) < 0.01f)
            {
                agent.TransitionToState(IdleState);
            }
        }

        protected void SetPlayerVelocity()
        {
            agent.rigidBody2D.velocity = movementData.currentVelocity;
        }

        protected void CalculateVelocity()
        {
            CalculateSpeed(agent.agentInput.MovementVector, movementData);
            CalculateHorizontalDirection(movementData);
            movementData.currentVelocity = Vector3.right * movementData.horizontalMovementDirection * movementData.currentSpeed;
            movementData.currentVelocity.y = agent.rigidBody2D.velocity.y;
        }

        protected void CalculateHorizontalDirection(MovementData movementData)
        {
            if (agent.agentInput.MovementVector.x > 0)
            {
                movementData.horizontalMovementDirection = 1;
            }
            else if (agent.agentInput.MovementVector.x < 0)
            {
                this.movementData.horizontalMovementDirection = -1;
            }
        }

        protected void CalculateSpeed(Vector2 movementVector, MovementData movementData)
        {
            if (Mathf.Abs(movementVector.x) > 0)
            {
                movementData.currentSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                movementData.currentSpeed -= deacceleration * Time.deltaTime;
            }

            movementData.currentSpeed = Mathf.Clamp(movementData.currentSpeed, 0, maxSpeed);
        }
    }
}
