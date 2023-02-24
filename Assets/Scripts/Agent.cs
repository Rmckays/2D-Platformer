using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public PlayerInput playerInput;
    public AgentAnimation agentAnimation;
    public AgentRenderer agentRenderer;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponentInParent<PlayerInput>();
        agentAnimation = GetComponentInChildren<AgentAnimation>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
    }

    private void Start()
    {
        playerInput.OnMovement += HandleMovement;
        playerInput.OnMovement += agentRenderer.FaceDirection;
    }

    private void HandleMovement(Vector2 input)
    {
        if (Math.Abs(input.x) > 0)
        {
            if (Math.Abs(rigidBody2D.velocity.x) < 0.01f)
            {
                agentAnimation.PlayAnimation(AnimationType.run);
            }
            rigidBody2D.velocity = new Vector2(input.x * 5, rigidBody2D.velocity.y);
        }
        else
        {
            if (Math.Abs(rigidBody2D.velocity.x) > 0)
            {
                agentAnimation.PlayAnimation(AnimationType.idle);
            }
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }
    }
    
    
}
