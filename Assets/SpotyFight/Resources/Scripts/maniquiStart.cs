using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maniquiStart : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private GameObject gm;

    void Start()
    {
        // Obtiene todos los Rigidbody hijos de este objeto
        rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
        SetEnabled(false);

        gm = GameObject.FindGameObjectWithTag("GameManager");

        // Asegurarse de que cada hijo tenga el script de detección de colisiones
        foreach (Rigidbody rb in rigidbodies)
        {
            Collider collider = rb.gameObject.GetComponent<Collider>();
            if (collider != null)
            {
                ragdollComponentsCollisionDetector detector = rb.gameObject.AddComponent<ragdollComponentsCollisionDetector>();
                detector.parentScript = this;
            }
        }
    }

    void SetEnabled(bool enabled)
    {
        bool isKinematic = !enabled;
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = isKinematic;
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetEnabled(true);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetEnabled(false);
        }
        */
    }

    public void OnChildCollisionEnter(Collision other)
    {
        // Método llamado por los hijos cuando detectan una colisión
        Debug.Log("Debería haberse llamado a este método");

        // Calcular la fuerza de la colisión
        float collisionForce = other.relativeVelocity.magnitude * other.rigidbody.mass;
        Debug.Log("Fuerza de colisión: " + collisionForce);
        SetEnabled(true);
        gm.GetComponent<GameManager>().startGame();
        Invoke("DestroyMySelf",20);
    }

    private void DestroyMySelf()
    {
        Destroy(gameObject);
    }
}