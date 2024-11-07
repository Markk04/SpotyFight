using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour
{
    // Called when another collider enters the trigger attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the tag "PlayerHands"
        if (other.CompareTag("Hands"))
        {
            Debug.Log("Player's hand has entered the trigger area!");
            // Add any additional logic for when the player's hand enters the trigger
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Any other code you want to run each frame
    }
}
