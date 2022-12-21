using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRoamingState : AIMovementState
{
    public AIRoamingState(AIMovementStateMachine aiMovementStateMachine) : base(aiMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.AI.Agent.isStopped = false;
        stateMachine.AI.Agent.updateRotation = false;
        StartAnimation(ai.AnimationData.MovingParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(ai.AnimationData.MovingParameterHash);
    }

    public override void Update()
    {
        base.Update();
        Rotate();
        Roam();
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if(collision.transform.CompareTag("Player"))
        {
            stateMachine.AI.Agent.updateRotation = false;
            stateMachine.ChangeState(stateMachine.BumpState);
        }
    }

    private void Rotate()
    {
        if (stateMachine.AI.Agent.remainingDistance >= 2f)
        {
            // ������Ʈ�� ȸ�� ��
            Vector3 direction = stateMachine.AI.Agent.desiredVelocity;

            // ȸ�� ���� ����
            Quaternion rotation = Quaternion.LookRotation(direction);

            // ���� �������� �Լ��� �ε巯�� ȸ�� ó��
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, rotation, Time.deltaTime * 10.0f);
        }
    }

    private void Roam()
    {
        if (ai.transform.position.DistanceFlat(ai.pathPoints[ai.Index].position) < ai.minDistance)
        {
            ai.Index = (ai.Index + 1) % ai.pathPoints.Length;
        }

        ai.Agent.SetDestination(ai.pathPoints[ai.Index].position);
    }
}
