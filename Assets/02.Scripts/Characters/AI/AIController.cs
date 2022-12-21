using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AIController : MonoBehaviour
{
    [field: SerializeField] public AIAnimationData AnimationData { get; private set; }

    private AIMovementStateMachine movementStateMachine;
    [SerializeField]
    private GameObject path;
    public Transform[] pathPoints { get; private set; }

    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }


    public float minDistance { get; private set; } = 4f;
    private int index = 0;

    [SerializeField]
    private bool reverse = false;
    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    private void Awake()
    {
        AnimationData.Initialize();

        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();

        movementStateMachine = new AIMovementStateMachine(this);
        pathPoints = new Transform[path.transform.childCount];
        pathPoints = path.transform.AllChildrenObjArrayT();

        if (reverse)
            Array.Reverse(pathPoints);
    }


    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.RoamingState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();

        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }
    private void OnTriggerEnter(Collider collider)
    {
        movementStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        movementStateMachine.OnTriggerExit(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        movementStateMachine.OnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        movementStateMachine.OnCollisionExit(collision);
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        movementStateMachine.OnAnimationEnterEvent();
    }

    public void OnMovementStateAnimationExitEvent()
    {
        movementStateMachine.OnAnimationExitEvent();
    }

    public void OnMovementStateAnimationTransitionEvent()
    {
        movementStateMachine.OnAnimationTransitionEvent();
    }
}
