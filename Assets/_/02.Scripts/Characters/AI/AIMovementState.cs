using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementState : IState
{
    protected AIMovementStateMachine stateMachine;
    protected AIController ai;
    public AIMovementState(AIMovementStateMachine aiMovementStateMachine) 
    {
        stateMachine = aiMovementStateMachine;
        ai = stateMachine.AI;
    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void OnAnimationEnterEvent()
    {

    }

    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTransitionEvent()
    {

    }

    public virtual void OnCollisionEnter(Collision collision)
    {

    }

    public virtual void OnCollisionExit(Collision collision)
    {

    }

    public virtual void OnTriggerEnter(Collider collider)
    {

    }

    public virtual void OnTriggerExit(Collider collider)
    {

    }
    public virtual void Update()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    protected void StartAnimation(int animationHash)
    {
        ai.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        ai.Animator.SetBool(animationHash, false);
    }
}
