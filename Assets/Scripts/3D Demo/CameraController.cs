using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1.5f, 0f);

    [Header("Orbit")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 60f;
    [SerializeField] private float startingPitch = 15f;

    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;

    private float yaw;
    private float pitch;
    private Vector2 lookInput;

    private void Start()
    {
        pitch = startingPitch;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void Update()
    {
        lookInput = lookAction.action.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        yaw += lookInput.x * sensitivity;
        pitch -= lookInput.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 focusPoint = target.position + targetOffset;
        Vector3 desiredPosition = focusPoint - rotation * Vector3.forward * distance;

        transform.position = desiredPosition;
        transform.rotation = rotation;
    }
}