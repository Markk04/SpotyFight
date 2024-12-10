using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    private Collider mannequinCollider;
    public int id;

    // Referencia al HitScript del "tío"
    private HitScript hitScript;

    void Start()
    {
        // Busca al "tío" del objeto actual
        Transform parent = transform.parent;
        if (parent != null)
        {
            Transform grandParent = parent.parent; // El abuelo del objeto actual
            if (grandParent != null)
            {
                // Busca el HitScript en los hijos del abuelo
                hitScript = grandParent.GetComponentInChildren<HitScript>();

                if (hitScript == null)
                {
                    Debug.LogError("No se encontró un HitScript en el tío.");
                }
            }
            else
            {
                Debug.LogError("El objeto no tiene un abuelo en la jerarquía.");
            }
        }
        else
        {
            Debug.LogError("El objeto no tiene un padre en la jerarquía.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider colliderTouched = collision.collider;
        if (colliderTouched != null && hitScript != null)
        {
            // Llama al método OtorgarColores del HitScript
            hitScript.OtorgarColores(id, hitScript.mainColor, false, 0);
            hitScript.isHitted(id);
        }
    }
}
