using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    //float dello sprint
    public float sprinting = 3f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //bool sprint
    bool isSprinting = false;

    //float jump
    public float jumpSpeed;
    private float ySpeed;
    private bool isGrounded;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    //movement con smooth turn
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
       
        //sprint + bool sopra
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting; 
            if (isSprinting)
            {
                speed += sprinting;
            }
            else
            {
                speed -= sprinting;
            }
        }

        

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButtonDown("jump"))
        {
            ySpeed = 0.5f;
        }

        if (controller.isGrounded)
        {
            ySpeed = 0.5f;
                isGrounded = true;
                if (Input.GetButtonDown("jump"))
                {
                 ySpeed = jumpSpeed;
                 isGrounded = false;
                }
        }
    }
}

