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

    //float salto
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    public Transform groundCheck;      // Assegna un oggetto vuoto appena sotto la pecora
    public float groundDistance = 0.2f; // Raggio del cerchio di controllo
    public LayerMask groundMask;       // Scegli il layer del terreno nell'Inspector

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


        //controller grounded
        isGrounded = controller.isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // tiene incollato al suolo
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        //gravità
        velocity.y += gravity * Time.deltaTime;

        // movement personaggio
        controller.Move(velocity * Time.deltaTime);

    }
}

