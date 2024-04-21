using UnityEngine;
using System.Collections;

public class ObjectDestructionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with the ground
        if (collision.gameObject.tag == "Ground")
        {
            // Start the coroutine to destroy this object after a delay
            StartCoroutine(DestroyAfterDelay(3f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy this object
        Destroy(gameObject);
    }
}
