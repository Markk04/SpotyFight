using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour
{
    public GameObject mannequin;  // Reference to the mannequin GameObject
    private Rigidbody rb;

    // Called once at the start
    private void Start()
    {
        // Check if mannequin has been assigned in the Inspector
        if (mannequin != null)
        {
            // Get the Rigidbody component of the mannequin GameObject
            rb = mannequin.GetComponent<Rigidbody>();

            // Make sure the mannequin has a Rigidbody and set it up for physics
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            else
            {
                Debug.LogWarning("The mannequin does not have a Rigidbody component!");
            }
        }
        else
        {
            Debug.LogWarning("Mannequin GameObject not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the tag "Hands"
        if (other.CompareTag("Hands"))
        {
            Debug.Log("Player's hand has entered the trigger area!");

            // Apply an upward and forward force to make the mannequin "leap"
            if (rb != null)
            {
                // Customize this force as needed for upward (y) and forward (z) directions
                Vector3 force = new Vector3(0, 20, 0);  // Modify these values as needed
                rb.AddForce(force, ForceMode.Impulse);  // Apply the force instantly
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Obtener la dirección del impacto
        Vector3 direction = collision.contacts[0].normal;

        // Invertir la dirección para que sea la dirección de salida
        Vector3 impulseDirection = -direction.normalized;

        // Obtener la magnitud de la fuerza del impacto
        float impactForce = collision.relativeVelocity.magnitude;

        // Aplicar la fuerza en la dirección del impacto
        rb.AddForce(impulseDirection * impactForce * 1, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // Any other code you want to run each frame
    }
}
