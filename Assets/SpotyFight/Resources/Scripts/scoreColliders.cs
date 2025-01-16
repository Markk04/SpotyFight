using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreColliders : MonoBehaviour
{
    private GameObject gameManager; 

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el trigger tiene el tag "Stop"
        if (other.CompareTag("ringExterior"))
        {
            gameManager.GetComponent<GameManager>().OnScoreColliderEnter();
        }
    }
}
