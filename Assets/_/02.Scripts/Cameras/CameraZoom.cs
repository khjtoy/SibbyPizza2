using Cinemachine;
using UnityEngine;


public class CameraZoom : MonoBehaviour
{
    [SerializeField] 
    [Range(0f, 12f), Header("�⺻ �Ÿ�")] private float defaultDistance = 6f;
    [SerializeField] 
    [Range(0f, 12f), Header("�ּ� �Ÿ�")] private float minimumDistance = 1f;
    [SerializeField] 
    [Range(0f, 12f), Header("�ִ� �Ÿ�")] private float maximumDistance = 6f;

    [SerializeField] 
    [Range(0f, 20f), Header("Zoom �ӵ�")] private float smoothing = 4f;
    [SerializeField] 
    [Range(0f, 20f), Header("Zoom ����")] private float zoomSensitivity = 1f;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    private float currentTargetDistance;

    private void Awake()
    {
        GetComponent<CinemachineInputProvider>().enabled = true;

        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();

        currentTargetDistance = defaultDistance;
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        // ��ǥ ���� ����
        if (currentDistance == currentTargetDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}