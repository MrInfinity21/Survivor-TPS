using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
public class AimStateManager : MonoBehaviour
{
    AimBaseState _currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity = 1f;
    float xAxis, yAxis;

    [HideInInspector] public Animator _anim;
    [HideInInspector] public CinemachineCamera vCam;
    public float _adsFov = 40f;
    [HideInInspector] public float _hipFov;
    [HideInInspector] public float _currentFov;
    public float _fovSmoothSpeed = 10f;

    [SerializeField] Transform _aimPos;
    [SerializeField] float _aimSmoothSpeed;
    [SerializeField] LayerMask _aimMask;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        vCam = GetComponentInChildren<CinemachineCamera>();
        _hipFov = vCam.Lens.FieldOfView;
        _anim = GetComponent<Animator>();
        SwitchState(Hip);

    }
    void Update()
    {
        
        RotatePlayerToCamera();
        _currentState.UpdateState(this);

        xAxis += Input.GetAxisRaw("Mouse X") * _mouseSensitivity;
        yAxis -= Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80f, 80f);

        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, _currentFov, _fovSmoothSpeed * Time.deltaTime);


        Vector2 screenCentre = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimMask))
            _aimPos.position = Vector3.Lerp(_aimPos.position, hit.point, _aimSmoothSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        RotateCameraPivot();
    }

    private void RotateCameraPivot()
    {
        if (_cameraPivot != null)
        {
            _cameraPivot.localEulerAngles = new Vector3(yAxis, 0f, 0f);
        }
        transform.eulerAngles = new Vector3(0f, xAxis, 0f);
    }

    private void RotatePlayerToCamera()
    {
        Vector3 viewDirection = _cameraPivot.forward;
        viewDirection.y = 0f;
        viewDirection.Normalize();

        if (viewDirection != Vector3.zero)
            transform.forward = viewDirection;  
    }

    public void SwitchState(AimBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
