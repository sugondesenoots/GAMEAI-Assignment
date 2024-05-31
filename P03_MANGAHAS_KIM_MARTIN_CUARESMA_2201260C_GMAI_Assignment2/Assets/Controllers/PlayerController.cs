using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private Transform cameraTransform; 

    private Rigidbody rb; 
    private Vector3 movementDir = Vector3.zero; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        //Get player input for movement in x & y directions
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        //Calculate forward & right vectors relative to the camera's orientation
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Doing this ignores the vertical component of the vectors 
        //This ensures that the player moves only horizontally
        forward.y = 0;
        right.y = 0;

        //Normalizing ensures consistent speed in all directions
        forward.Normalize();
        right.Normalize();

        //Calculates the movement direction by combining forward & right vectors based on player's inputs
        movementDir = (forward * movementY + right * movementX).normalized;
    }

    private void FixedUpdate()
    {
        //Move the player's rb based on the calculated movement direction & speed (shown above)
        rb.MovePosition(rb.position + movementDir * speed * Time.fixedDeltaTime);
    }
}
