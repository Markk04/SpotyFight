using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollComponentsCollisionDetector : MonoBehaviour
{
    // Referencia al script en el objeto padre
    public maniquiStart parentScript;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colisión detectada");
        if (other.gameObject.CompareTag("Hands"))
        {
            Debug.Log("Colisión detectada por un hijo con el tag 'Hands'");
            // Notificar al objeto padre sobre la colisión
            parentScript.OnChildCollisionEnter(other);
        }
    }
}
