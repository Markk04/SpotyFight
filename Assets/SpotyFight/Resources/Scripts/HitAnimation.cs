using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    private Collider[] animationColliders;  // Array to store colliders with the "Animation" tag.
    private Animator animator;              // Reference to the Animator component.
    private Rigidbody mannequinRigidbody;   // Reference to the mannequin's Rigidbody.

    // Name of the animation trigger or bool parameter
    public string animationTriggerName = "HitTrigger";

    void Start()
    {
        // Find all objects with the "Animation" tag and get their colliders.
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Animation");
        animationColliders = new Collider[objectsWithTag.Length];

        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            animationColliders[i] = objectsWithTag[i].GetComponent<Collider>();
        }

        // Get the Animator component on this GameObject
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("No Animator component found on " + gameObject.name);
        }

        // Get the Rigidbody component on this GameObject
        mannequinRigidbody = GetComponent<Rigidbody>();

        if (mannequinRigidbody != null)
        {
            // Make the Rigidbody kinematic to prevent movement
            mannequinRigidbody.isKinematic = true;
        }
        else
        {
            Debug.LogWarning("No Rigidbody component found on " + gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Log a message whenever a collision occurs
        Debug.Log(gameObject.name + " collided with: " + collision.gameObject.name);

        // Log the details of the collider being touched
        Collider colliderTouched = collision.collider;
        if (colliderTouched != null)
        {
            Debug.Log("Touched collider: " + colliderTouched.name + " on object: " + colliderTouched.gameObject.name);
        }

        // Check if the collided object is one of the animation colliders
        foreach (Collider col in animationColliders)
        {
            if (collision.collider == col)
            {
                // Trigger the animation if an animator is present
                if (animator != null)
                {
                    animator.SetTrigger(animationTriggerName);  // Play the animation on collision
                    Debug.Log("Playing animation on collision with: " + collision.gameObject.name);
                }
                break;  // Exit the loop once the matching collider is found
            }
        }
    }

    // Add this method to log when the trigger is touched
    private void OnTriggerEnter(Collider other)
    {
        // Log the details of the collider being touched
        if (other != null)
        {
            Debug.Log("Touched trigger collider: " + other.name + " on object: " + other.gameObject.name);
        }

        // Check if the entered trigger is one of the animation colliders
        foreach (Collider col in animationColliders)
        {
            if (other == col)
            {
                // Trigger the animation if an animator is present
                if (animator != null)
                {
                    animator.SetTrigger(animationTriggerName);  // Play the animation on trigger
                    Debug.Log("Trigger collision, playing animation with: " + other.gameObject.name);
                }
                break;  // Exit the loop once the matching collider is found
            }
        }
    }
}
