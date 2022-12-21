using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBumpState : AIMovementState
{
    public AIBumpState(AIMovementStateMachine aiMovementStateMachine) : base(aiMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        ai.Agent.isStopped = true;
        StartAnimation(ai.AnimationData.HitParameterHash);
        CameraShake.Instance.ShakeCamera(0.5f, 0.3f);
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();
        StopAnimation(ai.AnimationData.HitParameterHash);
        stateMachine.ChangeState(stateMachine.RoamingState);
    }
}
