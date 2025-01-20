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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ragdollManiquin"))
        {
            Debug.Log("Score collider hit");
            gameManager.GetComponent<GameManager>().OnScoreColliderEnter();
        }
    }
}
