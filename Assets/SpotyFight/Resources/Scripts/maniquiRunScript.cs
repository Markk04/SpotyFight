using UnityEngine;

public class ManiquiRunScript : MonoBehaviour
{
    private float forwardSpeed = 2f;   // Velocidad hacia adelante

    void Update()
    {
        // Movimiento hacia adelante
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    // Este método se ejecuta cuando entra en un trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el trigger tiene el tag "Stop"
        if (other.CompareTag("Stop"))
        {
            forwardSpeed = 0f; // Detener el movimiento estableciendo la velocidad a 0
        }
    }

}
