using Player_Movements.MovementStates;
using UnityEngine;

[RequireComponent(typeof(InputController))] //this will force you to have an input controller
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

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    private Vector2 _movementDirection;
   

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

    public void SetDirection(Vector2 direction)
    {
        hzInput = direction.x;
        vInput = direction.y;
        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir.normalized * currentMoveSpeed *  Time.deltaTime);
    }
    void GetDirectionAndMove()
    {
        //hzInput = Input.GetAxis("Horizontal");
        //vInput = Input.GetAxis("Vertical");


    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity *  Time.deltaTime);
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    
      
    
    }



}
