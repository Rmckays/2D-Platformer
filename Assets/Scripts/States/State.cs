using UnityEngine;
using UnityEngine.Events;

namespace States
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] protected State JumpState;
        protected Agent agent;

        public UnityEvent OnEnter, OnExit;

        public void InitializeState(Agent agent)
        {
            this.agent = agent;
        }

        public void Enter()
        {
            this.agent.agentInput.OnAttack += HandleAttack;
            this.agent.agentInput.OnJumpPressed += HandleJumpPressed;
            this.agent.agentInput.OnJumpReleased += HandleJumpReleased;
            this.agent.agentInput.OnMovement += HandleMovement;
            OnEnter?.Invoke();
            EnterState();
        }

        protected virtual void EnterState()
        {
        }

        protected virtual void HandleMovement(Vector2 obj)
        {
        }

        protected virtual void HandleJumpReleased()
        {
        }

        protected virtual void HandleJumpPressed()
        {
            TestJumpTransition();
        }

        private void TestJumpTransition()
        {
            if (agent.groundDetector.isGrounded)
            {
                agent.TransitionToState(JumpState);
            }
        }

        protected virtual void HandleAttack()
        {
        }
    
        public virtual void StateUpdate(){}
    
        public virtual void StateFixedUpdate(){}

        public void Exit()
        {
            this.agent.agentInput.OnAttack -= HandleAttack;
            this.agent.agentInput.OnJumpPressed -= HandleJumpPressed;
            this.agent.agentInput.OnJumpReleased -= HandleJumpReleased;
            this.agent.agentInput.OnMovement -= HandleMovement;
            OnExit?.Invoke();
            ExitState();
        }

        protected virtual void ExitState()
        {
        }
    }
}
