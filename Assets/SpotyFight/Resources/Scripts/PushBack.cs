using UnityEngine;

public class PushBack : MonoBehaviour
{
    public float jumpForce = 100f; // Adjust this to control how hard the mannequin "jumps" on hit.
    private Rigidbody mannequinRb;  // Rigidbody of the mannequin.
    private Collider mannequinCollider;  // Collider of the mannequin.

    // This can be replaced by any condition you'd like.
    public bool enablePushback = false; // Set this externally or toggle it based on your condition.

    void Start()
    {
        // Get the Rigidbody and Collider components on this GameObject (mannequin).
        mannequinRb = GetComponent<Rigidbody>();
        mannequinCollider = GetComponent<Collider>();

        // Check if the mannequin has a Rigidbody, and if so, make sure it’s set up correctly.
        mannequinRb.useGravity = true;
        mannequinRb.isKinematic = false;

        // Disable the collider at the start.
        if (mannequinCollider != null)
        {
            mannequinCollider.enabled = false;
        }
    }

    private void Update()
    {
        // Check if the condition to enable pushback is met.
        if (enablePushback && mannequinCollider != null)
        {
            mannequinCollider.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only process the collision if the collider is enabled.
        if (mannequinCollider.enabled && collision.gameObject.CompareTag("Hands"))
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
