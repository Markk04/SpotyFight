using UnityEngine;

public class ManiquiRunScript : MonoBehaviour
{
    private float forwardSpeed = 2f;   // Velocidad hacia adelante
    private Animator animator; // Referencia al componente Animator
    private bool canMove = false; // Indica si el movimiento puede comenzar
    private bool isJumping = false; // Indica si se está realizando el salto
    private float jumpSpeed = 10f; // Velocidad de movimiento hacia arriba durante el salto
    private float jumpDelay = 1.25f; // Retardo antes de comenzar el salto
    private Rigidbody rb; // Referencia al Rigidbody

    void Start()
    {
        // Obtener el componente Animator del objeto hijo "Armature"
        Transform armatureTransform = transform.Find("Armature");
        if (armatureTransform != null)
        {
            animator = armatureTransform.GetComponent<Animator>();
        }

        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // Asegurarse de que la gravedad esté habilitada al inicio
        }
        
        // Verificar si el Animator no es nulo y activar la animación con un trigger
        if (animator != null)
        {
            animator.SetTrigger("corre"); // Asume que el trigger de la animación se llama "corre"
        }
    }

    void Update()
    {
        // Verificar si la animación "mecorroooo" está en ejecución antes de permitir el movimiento
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("mecorroooo"))
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        // Movimiento hacia adelante solo si se permite
        if (canMove)
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        }

        // Movimiento hacia arriba durante el salto
        if (isJumping)
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
        }
    }

    // Este método se ejecuta cuando entra en un trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el trigger tiene el tag "Stop"
        if (other.CompareTag("Stop"))
        {
            forwardSpeed = 0f; // Detener el movimiento estableciendo la velocidad a 0
            canMove = false;

            // Hacer la animación con trigger "salta" y mover hacia arriba
            if (animator != null)
            {
                animator.SetTrigger("salta"); // Asume que el trigger de la animación se llama "salta"
                Invoke("StartJumping", jumpDelay); // Iniciar el salto después del retardo
            }
        }
    }

    private void StartJumping()
    {
        if (rb != null)
        {
            rb.useGravity = false; // Desactivar la gravedad para permitir que el personaje suba indefinidamente
        }
        isJumping = true; // Permitir el movimiento hacia arriba
    }
}
