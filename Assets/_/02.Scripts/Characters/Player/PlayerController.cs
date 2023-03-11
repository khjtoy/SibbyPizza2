using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerResizableCapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Collisions")]
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

    [field: Header("Camera")]
    [field: SerializeField] public PlayerCameraRecenteringUtility CameraRecenteringUtility { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    public PlayerInput Input { get; private set; } // �÷��̾� �Է�
    public PlayerResizableCapsuleCollider ResizableCapsuleCollider { get; private set; }

    public Transform MainCameraTransform { get; private set; } // ���� ī�޶� ĳ��

    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        CameraRecenteringUtility.Initialize();
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();

        Input = GetComponent<PlayerInput>();
        ResizableCapsuleCollider = GetComponent<PlayerResizableCapsuleCollider>();

        MainCameraTransform = Camera.main.transform;

        movementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void Start()
    {
        // ù ��° ���¸� Idling ���·� ����
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();

        movementStateMachine.Update();

        if(UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            transform.SetPositionY(1);
        }
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        movementStateMachine.OnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        movementStateMachine.OnCollisionExit(collision);
    }

    private void OnTriggerEnter(Collider collider)
    {
        movementStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        movementStateMachine.OnTriggerExit(collider);
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