using UnityEngine;

public class CamMovementScript : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float range = 10f; // Distancia máxima a moverse desde el punto inicial

    private Vector3 startPosition;

    void Start()
    {
        // Guardamos la posición inicial de la cámara
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculamos el desplazamiento en función del tiempo y del rango
        float offset = Mathf.Sin(Time.time * speed) * range;

        // Movemos la cámara de un lado al otro en el eje X
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
