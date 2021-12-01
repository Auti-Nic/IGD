using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float moveSpeed;
    public Transform cam;
    [SerializeField] private float walkSpeed ;
    [SerializeField] private float turnSmoothTime ;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private CharacterController controller;
    

    //ground check
    [SerializeField] private bool isGrounded;
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool canJump = true;

    //global variables 
    float turnSmoothVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        Move();
        
    }

    private void Move()
    {

        isGrounded = Physics.CheckSphere(transform.position, GroundCheckDistance, groundMask);
        

        //stop applying gravity when grounded
        if (isGrounded && velocity.y < 0)
        {        
            velocity.y = -2f;
        }
        

        //get input 

        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector3(moveX,0,moveZ).normalized;


        //smooth turning  
       
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        
        //move to lookat direction
        Vector3 lookatDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        
            
        if (moveDirection == Vector3.zero)
        {
                //idle 
                Idle();
        }
        else if (moveDirection != Vector3.zero)
        {

                //walk to look at direction
                
            Walk();
            lookatDir = lookatDir.normalized * moveSpeed;
            controller.Move(lookatDir * Time.deltaTime);
        }
       

        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded )
        {
            Jump();
        }
            
            
        

        
        //controller.Move(moveDirection * Time.deltaTime);

        //calculat gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
         
    }


    //only set up speed an animator in following functions
    private void Idle()
    {

    }

    private void Walk()
    {
        moveSpeed = walkSpeed;

    }

    private void Jump()
    {
        //V^2 - V^2(inital) = 2ax
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);  
    }
 

}
