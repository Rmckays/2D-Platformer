using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public PlayerInput playerInput;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnMovement += HandleMovement;
    }

    private void HandleMovement(Vector2 input)
    {
        if (Math.Abs(input.x) > 0)
        {
            rigidBody2D.velocity = new Vector2(input.x * 5, rigidBody2D.velocity.y);
        }
        else
        {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }
    }
}
