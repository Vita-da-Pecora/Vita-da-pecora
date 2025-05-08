using UnityEngine;

public class PROVAmovimento : MonoBehaviour
{
    public float moveSpeed = 5f; // velocità di movimento
    public float rotationSpeed = 700f; // velocità di rotazione

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical"); 

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (move.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
