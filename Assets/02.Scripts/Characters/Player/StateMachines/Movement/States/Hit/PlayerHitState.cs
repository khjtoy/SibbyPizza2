using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitState : PlayerGroundedState
{
    public PlayerHitState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DataManager.Instance.CurrentUser.currentPizza = "";
        UIManager.Instance.SetPizzaText();
        StopAnimation(stateMachine.Player.AnimationData.GroundedParameterHash);
        StartAnimation(stateMachine.Player.AnimationData.HitParameterHash);

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVelocity();

        CameraShake.Instance.ShakeCamera(3f, 0.4f);
        // stateMachine.Player.Rigidbody.isKinematic = true;

        SoundManager.Instance.Play("CarCrash");
    }

    public override void OnAnimationTransitionEvent()
    {
        base.OnAnimationTransitionEvent();
        stateMachine.ChangeState(stateMachine.IdlingState);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.HitParameterHash);
        // stateMachine.Player.Rigidbody.isKinematic = false;

        SoundManager.Instance.Stop("CarCrash");
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {

    }
}
