using UnityEngine;

public class HitReaction : MonoBehaviour
{
    public float jumpForce = 50f; // Adjust this to control how hard the mannequin "jumps" on hit.
    private Rigidbody mannequinRb;  // Rigidbody of the mannequin.

    void Start()
    {
        // Get the Rigidbody component on this GameObject (mannequin).
        mannequinRb = GetComponent<Rigidbody>();

        // Check if the mannequin has a Rigidbody, and if so, make sure it’s set up correctly.
        mannequinRb.useGravity = true;
        mannequinRb.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object that collided with the mannequin has the "Hands" tag.
        if (collision.gameObject.CompareTag("Hands"))
        {
            // Get the contact point to determine a more accurate impact direction.
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 impactDirection = (transform.position - contactPoint).normalized;

            // Apply force to the mannequin's Rigidbody in the direction of the impact.
            if (mannequinRb != null)
            {
                mannequinRb.AddForce(impactDirection * jumpForce, ForceMode.Impulse);
                Debug.Log("Applied force with direction: " + impactDirection);
            }
        }
    }
}
