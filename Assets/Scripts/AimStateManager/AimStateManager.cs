using UnityEngine;
using Unity.Cinemachine;
public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float mouseSensitivity = 1f;
    float xAxis, yAxis;

    [HideInInspector] public Animator anim;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);

    }
    void Update()
    {
        HandleInput();
        RotatePlayerToCamera();

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        RotateCameraPivot();
    }

    private void HandleInput()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80f, 80f);
    }

    private void RotateCameraPivot()
    {
        if (cameraPivot != null)
        {
            cameraPivot.localEulerAngles = new Vector3(yAxis, 0f, 0f);
        }
        transform.eulerAngles = new Vector3(0f, xAxis, 0f);
    }

    private void RotatePlayerToCamera()
    {
        Vector3 viewDirection = cameraPivot.forward;
        viewDirection.y = 0f;
        viewDirection.Normalize();

        if (viewDirection != Vector3.zero)
            transform.forward = viewDirection;  
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
