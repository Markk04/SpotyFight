using UnityEngine;

public class HitCartoonAnimation : MonoBehaviour
{
    public Sprite targetSprite; // El sprite que se usar�.
    public float scaleDuration = 0.4f; // Duraci�n del efecto de escalado.
    public float delayBeforeShrink = 0f; // Tiempo antes de volver a hacerse peque�o.

    private Camera cam;

    private SpriteRenderer spriteRenderer; // Componente SpriteRenderer.
    private Vector3 originalScale;   // Escala original.
    private float scaleTimer = 0f;   // Temporizador para controlar el escalado.
    private bool isScaling = false; // Controla si est� en proceso de escalado.
    private bool scalingUp = true;  // Controla si est� escalando hacia arriba o hacia abajo.

    void Start()
    {
        // Obtener el SpriteRenderer del objeto actual.
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        if (spriteRenderer == null)
        {
            Debug.LogError("El objeto no tiene un SpriteRenderer.");
            return;
        }

        // Ajustar la escala inicial.
        originalScale = Vector3.one/10; // Escala inicial como "normal".
        transform.localScale = Vector3.zero; // Comienza en escala 0.

        // Asegurarse de que el sprite est� inicialmente oculto.
        spriteRenderer.enabled = false;

        // Asignar el sprite inicial
        if (targetSprite != null)
        {
            spriteRenderer.sprite = targetSprite;
        }
        else
        {
            Debug.LogWarning("No se ha asignado un sprite a 'targetSprite'.");
        }
        transform.LookAt(cam.transform);

        StartScaling(true);

    }

    void Update()
    {

        // Si est� en proceso de escalado
        if (isScaling)
        {
            // Continuar el escalado
            scaleTimer += Time.deltaTime;
            float progress = scaleTimer / scaleDuration;
            transform.localScale = Vector3.Lerp(
                scalingUp ? Vector3.zero : originalScale,
                scalingUp ? originalScale : Vector3.zero,
                progress
            );

            // Finalizar el escalado si el tiempo se completo
            if (scaleTimer >= scaleDuration)
            {
                scaleTimer = 0f;

                if (scalingUp)
                {
                    if (delayBeforeShrink > 0f)
                    {
                        //nose
                    }
                    else
                    {
                        StartScaling(false);
                    }
                }
                else
                {
                    spriteRenderer.enabled = false; // Ocultar el sprite al finalizar el escalado hacia abajo.
                    isScaling = false;
                }
            }
        }
    }

    void StartScaling(bool up)
    {
        isScaling = true;
        scalingUp = up;
        scaleTimer = 0f;

        if (up)
        {
            spriteRenderer.enabled = true; // Mostrar el sprite al comenzar el escalado hacia arriba.
        }else{
            Destroy(gameObject);
        }


    }
}
