using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    [HideInInspector] public Vector3 dir;
    public float moveSpeed = 3;
    float hzInput, vInput;
    CharacterController controller;
    #endregion

    #region Ground Flag
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] float gravity = -9.8f;
    Vector3 velocity;
    #endregion

    MovementBaseState currentState;
  
    public int HzInputHash { get; private set; } = Animator.StringToHash("hzInput");
    public int VInputHash { get; private set; } = Animator.StringToHash("vInput");
    public int WalkHash { get; private set; } = Animator.StringToHash("Walking");

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();

    [HideInInspector] public Animator anim;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();


        SwitchState(Idle);
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat(HzInputHash, hzInput);
        anim.SetFloat(VInputHash, vInput);

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
        float hzInputRaw = Input.GetAxisRaw("Horizontal");
        float vInputRaw = Input.GetAxisRaw("Vertical");

        dir = transform.forward * vInputRaw + transform.right * hzInputRaw;

        controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) 
            return true;

        return false;
    }

    void Gravity()
    {
        if (IsGrounded()) 
            return;

        velocity.y += gravity * Time.deltaTime;
        if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }

}
