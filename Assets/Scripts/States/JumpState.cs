using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class JumpState : MovementState
{
    public float jumpForce = 12f;
    public float lowJumpMultiplier = 2;
    private bool jumpPressed = false;
    public State FallState;
    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.jump);
        movementData.currentVelocity = agent.rigidBody2D.velocity;
        movementData.currentVelocity.y = jumpForce;
        agent.rigidBody2D.velocity = movementData.currentVelocity;
        jumpPressed = true;
    }

    protected override void HandleJumpPressed()
    {
        jumpPressed = true;
    }

    protected override void HandleJumpReleased()
    {
        jumpPressed = false;
    }

    public override void StateUpdate()
    {
        ControlJumpHeight();
        CalculateVelocity();
        SetPlayerVelocity();
        if (agent.rigidBody2D.velocity.y <= 0)
        {
            agent.TransitionToState(FallState);
        }
    }

    private void ControlJumpHeight()
    {
        if (jumpPressed == false)
        {
            movementData.currentVelocity = agent.rigidBody2D.velocity;
            movementData.currentVelocity.y += lowJumpMultiplier*Physics2D.gravity.y * Time.deltaTime;
            agent.rigidBody2D.velocity = movementData.currentVelocity;
        }
    }
}
