using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float mouseSensitivity = 1f;
    float xAxis, yAxis;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineCamera vCam;
    public float adsFov = 40f;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10f;

    [SerializeField] Transform aimPos;
    [SerializeField] float aimSmoothSpeed;
    [SerializeField] LayerMask aimMask;
    private Vector2 _aimDirection;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        vCam = GetComponentInChildren<CinemachineCamera>();
        hipFov = vCam.Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
        _aimDirection = Vector2.zero;

    }

    public void HandleAimDirection(Vector2 direction)
    {
        _aimDirection = direction;
        Debug.Log($"We got the direction of {direction}");
    }
    void Update()
    {
        
        RotatePlayerToCamera();
        currentState.UpdateState(this);

        xAxis += _aimDirection.x * mouseSensitivity;
        yAxis -= _aimDirection.y * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80f, 80f);

        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);


        Vector2 screenCentre = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        RotateCameraPivot();
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
