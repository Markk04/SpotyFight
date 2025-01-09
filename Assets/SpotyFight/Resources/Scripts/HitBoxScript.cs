using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    private Collider mannequinCollider;
    public int id;

    public GameObject cartoonEffect;
    private GameObject gm;

    // Referencia al HitScript del "tío"
    private HitScript hitScript;

    // Referencia al jugador (debe asignarse desde el inspector o buscarse)
    private Transform player;

    void Start()
    {
        // Busca al "tío" del objeto actual
        player = GameObject.FindGameObjectWithTag("jugador").transform;
        Transform parent = transform.parent;
        gm = GameObject.FindGameObjectWithTag("GameManager");
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
            if(hitScript.isHitted(id)){
                // Instancia el efecto
                if (cartoonEffect != null && player != null)
                {
                    // Instancia el efecto con la rotación correcta
                    Instantiate(cartoonEffect, transform.position, new Quaternion(0.0f,0.0f,0.0f,1));
                    gm.GetComponent<GameManager>().sumScore(1);
                }
                else
                {
                    Debug.LogError("CartoonEffect o Player no están asignados.");
                }
            }
        }
    }
}
