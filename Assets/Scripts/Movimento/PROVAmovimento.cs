using UnityEngine;

public class PROVAmovimento : MonoBehaviour
{
    public float moveSpeed = 5f; // velocità di movimento
    public float rotationSpeed = 700f; // velocità di rotazione
    public float jumpForce = 5f; // forza del salto

    private Rigidbody _rigidbody;
    private bool isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveHorizontal = new Vector3(move.x, 0f, move.z);

        transform.Translate(moveHorizontal * moveSpeed * Time.deltaTime, Space.World);

        if (moveHorizontal.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveHorizontal, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}

