using UnityEngine;
using Unity.Cinemachine;
public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] private Transform cameraTransform;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineCamera fCam;
    public float adsFov = 40;

    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10f;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        fCam = GetComponentInChildren<CinemachineCamera>();
        hipFov = fCam.Lens.FieldOfView;
        currentFov = hipFov;

        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);

    }
    void Update()
    {
        RotatePlayerToCamera();

        currentState.UpdateState(this);

        fCam.Lens.FieldOfView = Mathf.Lerp(fCam.Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);
    }

    private void RotatePlayerToCamera()
    {
        Vector3 viewDirection = cameraTransform.forward;
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
