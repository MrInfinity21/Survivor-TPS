using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public Vector3 dir;
    public float hzInput, vInput;
    CharacterController controller;
    #endregion

    [SerializeField] float _groundYOffset;
    [SerializeField] LayerMask _groundMask;
    Vector3 _spherePos;

    [SerializeField] float _gravity = -9.81f;
    Vector3 _velocity;

   

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator animator;



    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        animator.SetFloat("hzInput", hzInput);
        animator.SetFloat("vInput", vInput);
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this); 
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir.normalized * currentMoveSpeed *  Time.deltaTime);
    }

    bool IsGrounded()
    {
        _spherePos = new Vector3(transform.position.x, transform.position.y - _groundYOffset, transform.position.z);
        if (Physics.CheckSphere(_spherePos, controller.radius - 0.05f, _groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) _velocity.y += _gravity * Time.deltaTime;
        else if (_velocity.y < 0) _velocity.y = -2;

        controller.Move(_velocity *  Time.deltaTime);
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spherePos, controller.radius - 0.05f);
    }



}
