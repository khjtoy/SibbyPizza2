using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdlingState : IState
{
    protected AIMovementStateMachine stateMachine;
    public AIIdlingState(AIMovementStateMachine aiMovementStateMachine)
    {
        stateMachine = aiMovementStateMachine;
    }

    #region State Method
    public void Enter()
    {
        stateMachine.AI.Agent.isStopped = true;
        stateMachine.AI.Animator.SetBool(stateMachine.AI.AnimationData.MovingParameterHash, false);
    }
    public void Update()
    {
        
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void OnAnimationEnterEvent()
    {
    }

    public void OnAnimationExitEvent()
    {
    }

    public void OnAnimationTransitionEvent()
    {
    }

    public void OnTriggerEnter(Collider collider)
    {
    }

    public void OnTriggerExit(Collider collider)
    {
    }

    public void PhysicsUpdate()
    {
    }

    public void OnCollisionEnter(Collision collision)
    {

    }

    public void OnCollisionExit(Collision collision)
    {

    }
    #endregion
}
