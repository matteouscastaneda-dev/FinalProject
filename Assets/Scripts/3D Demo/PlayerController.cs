using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("References")]
    [SerializeField] private Transform cameraRig;
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody body;
    private Vector2 moveInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 cameraForward = cameraRig.forward;
        Vector3 cameraRight = cameraRig.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 desired = cameraForward * moveInput.y + cameraRight * moveInput.x;
        desired = Vector3.ClampMagnitude(desired, 1f);

        Vector3 velocity = desired * moveSpeed;
        velocity.y = body.linearVelocity.y;
        body.linearVelocity = velocity;

        if (desired.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desired);
            body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
}