using System;
using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;
using UnityEngine.Serialization;

public class Agent : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public PlayerInput agentInput;
    public AnimationManager animationManager;
    public GroundDetector groundDetector;
    public AgentRenderer agentRenderer;
    public State currentState = null;
    public State previousState = null;
    public State idleState;

    [Header("State Debugging")]
    public string stateName = "";

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        agentInput = GetComponentInParent<PlayerInput>();
        animationManager = GetComponentInChildren<AnimationManager>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        groundDetector = GetComponentInChildren<GroundDetector>();
        State[] states = GetComponentsInChildren<State>();
        foreach (var state in states)
        {
            Debug.Log(state.name);
            state.InitializeState(this);
        }
    }

    private void Start()
    {
        agentInput.OnMovement += agentRenderer.FaceDirection;
        TransitionToState(idleState);
    }

    public void TransitionToState(State desiredState)
    {
        if (desiredState == null) return;
        if (currentState != null)
        {
            currentState.Exit();
        }

        previousState = currentState;
        currentState = desiredState;
        currentState.Enter();

        DisplayState();
    }

    private void DisplayState()
    {
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        groundDetector.CheckIsGround();
        currentState.StateFixedUpdate();
    }
}
