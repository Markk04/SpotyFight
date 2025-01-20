using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gm;
    private GameObject player;
    private GameObject ragdollHead;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("jugador");

        // Obtener el collider del player y todos los colliders del ragdoll
        Collider playerCollider = player.GetComponent<Collider>();
        Collider[] ragdollColliders = GetComponentsInChildren<Collider>();

        // Ignorar las colisiones entre el collider del player y todos los colliders del ragdoll
        foreach (Collider ragdollCollider in ragdollColliders)
        {
            Physics.IgnoreCollision(ragdollCollider, playerCollider, true);
        }

        Invoke("DestroyMySelf", 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyMySelf(){
        Destroy(gameObject);
    }
}