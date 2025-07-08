using UnityEngine;
using Unity.Cinemachine;
public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] private Transform cameraTransform;

    [HideInInspector] public Animator anim;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);

    }
    void Update()
    {
        RotatePlayerToCamera();

        currentState.UpdateState(this);
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
