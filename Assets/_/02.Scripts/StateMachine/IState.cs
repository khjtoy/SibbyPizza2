using UnityEngine;

/// <summary>
/// State�� ���� �Լ� ����
/// </summary>
public interface IState
{
    public void Enter(); // �ش� State�� ���Դ°�
    public void Exit(); // �ش� State�� �����°�
    public void HandleInput(); // �Է�
    public void Update(); // �ݺ�
    public void PhysicsUpdate(); // �������� �ݺ�(FixedUpdate)
    public void OnTriggerEnter(Collider collider);
    public void OnTriggerExit(Collider collider);
    public void OnCollisionEnter(Collision collision);
    public void OnCollisionExit(Collision collision);
    public void OnAnimationEnterEvent();
    public void OnAnimationExitEvent();
    public void OnAnimationTransitionEvent();
}