using Unity.VisualScripting;
using UnityEngine;

public class ManiquiRunScript : MonoBehaviour
{
    private float forwardSpeed = 2f;   // Velocidad hacia adelante
    private Animator animator; // Referencia al componente Animator
    private bool canMove = false; // Indica si el movimiento puede comenzar
    private bool isJumping = false; // Indica si se está realizando el salto
    private float jumpSpeed = 15f; // Velocidad de movimiento hacia arriba durante el salto
    private float jumpDelay = 1.25f; // Retardo antes de comenzar el salto
    private Rigidbody rb; // Referencia al Rigidbody
    public GameObject maniquinPrefab; // Arrastra aquí tu prefab desde el inspector
    private float attractionSpeed = 17f; // Velocidad de atracción
    private GameObject target; // El objetivo al que el muñeco se va a mover
    private bool canAtraction = false; // Indica si la atracción es posible
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
            animator.SetTrigger("corre");
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

        // Verificar si la animación "quietoParao" está en ejecución
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("quietoParao"))
        {
            Debug.Log("El GameObject está en la animación 'quietoParao'.");


        //Crear el gameObject de manikin para golpiar
        GameObject maniquin = Instantiate(maniquinPrefab, gameObject.transform.position, Quaternion.identity);
        maniquin.GetComponentInChildren<HitScript>().setTarget(target);

        // Desactivar el GameObject que tiene el script (esto desactiva todo el objeto)
        Destroy(gameObject);
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

        // Si hay un objetivo de atracción, mover al muñeco hacia él
        if (target != null && canAtraction)
        {
            // Mover el muñeco hacia el objetivo de manera continua
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, attractionSpeed * Time.deltaTime);
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
        // Verifica si el trigger tiene el tag "Fall"
        if (other.CompareTag("Fall"))
        {
            jumpSpeed = 0f; // Detener el movimiento estableciendo la velocidad a 0
            canMove = false;
            Debug.Log("salta");

            // Hacer la animación con trigger "falling" y mover hacia abajo
            if (animator != null)
            {
                animator.SetTrigger("falling");
                Invoke("doCanAtraction",0.7f);
                Debug.Log("saltando");
            }

        }
        // Verifica si el trigger tiene el tag "Fall"
        if (other.CompareTag("attractionTarget"))
            {
                rb.useGravity = true;
                attractionSpeed = 0f;
                canMove = false;

                // Desactivar el BoxCollider de 'other'
                BoxCollider boxCollider = other.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;  // Desactiva el BoxCollider
                }

                // Hacer la animación con trigger "levanta" y mover hacia arriba
                if (animator != null)
                {
                    animator.SetTrigger("levanta");
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

    public void setTarget(GameObject target)
    {
        this.target = target;
    }
    private void doCanAtraction()
    {
        canAtraction = true;
    }
}
