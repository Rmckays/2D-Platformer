using System;
using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;
using UnityEngine.Serialization;

public class Agent : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    [FormerlySerializedAs("playerInput")] public PlayerInput agentInput;
    [FormerlySerializedAs("agentAnimation")] public AnimationManager animationManager;
    public AgentRenderer agentRenderer;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        agentInput = GetComponentInParent<PlayerInput>();
        animationManager = GetComponentInChildren<AnimationManager>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
    }

    private void Start()
    {
        agentInput.OnMovement += HandleMovement;
        agentInput.OnMovement += agentRenderer.FaceDirection;
    }

    private void HandleMovement(Vector2 input)
    {
        if (Math.Abs(input.x) > 0)
        {
            if (Math.Abs(rigidBody2D.velocity.x) < 0.01f)
            {
                animationManager.PlayAnimation(AnimationType.run);
            }
            rigidBody2D.velocity = new Vector2(input.x * 5, rigidBody2D.velocity.y);
        }
        else
        {
            if (Math.Abs(rigidBody2D.velocity.x) > 0)
            {
                animationManager.PlayAnimation(AnimationType.idle);
            }
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }
    }

    public void TransitionToState(State desiredState, State currentState)
    {
        throw new NotImplementedException();
    }
}
