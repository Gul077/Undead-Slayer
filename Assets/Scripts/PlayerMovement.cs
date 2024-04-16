using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; // Assuming you're using CharacterController for player movement

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Example: Check for player movement inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player
        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        controller.Move(movement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the barrier
        if (other.CompareTag("Barrier"))
        {
            // Debug message to check if OnTriggerEnter is being called
            Debug.Log("Player collided with the barrier!");

            // If the player collides with the barrier, stop their movement
            controller.Move(-controller.velocity * Time.deltaTime);
        }
    }
}
