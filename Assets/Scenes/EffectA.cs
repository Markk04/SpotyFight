using UnityEngine;

public class EffectA : MonoBehaviour
{
    public Sprite targetSprite; // El sprite que se usará.
    public float scaleDuration = 0.4f; // Duración del efecto de escalado.
    public float delayBeforeShrink = 0f; // Tiempo antes de volver a hacerse pequeño.

    private SpriteRenderer spriteRenderer; // Componente SpriteRenderer.
    private Vector3 originalScale;   // Escala original.
    private float scaleTimer = 0f;   // Temporizador para controlar el escalado.
    private bool isScaling = false; // Controla si está en proceso de escalado.
    private bool scalingUp = true;  // Controla si está escalando hacia arriba o hacia abajo.

    void Start()
    {
        // Obtener el SpriteRenderer del objeto actual.
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("El objeto no tiene un SpriteRenderer.");
            return;
        }

        // Ajustar la escala inicial.
        originalScale = Vector3.one; // Escala inicial como "normal".
        transform.localScale = Vector3.zero; // Comienza en escala 0.

        // Asegurarse de que el sprite esté inicialmente oculto.
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
    }

    void Update()
    {
        // Verificar si se presiona la tecla de espacio y si no se está escalando
        if (Input.GetKeyDown(KeyCode.Space) && !isScaling && spriteRenderer != null)
        {
            StartScaling(true); // Iniciar escalado hacia arriba.
        }

        // Si está en proceso de escalado
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

            // Finalizar el escalado si el tiempo se completó
            if (scaleTimer >= scaleDuration)
            {
                scaleTimer = 0f;

                if (scalingUp)
                {
                    if (delayBeforeShrink > 0f)
                    {
                        Invoke(nameof(StartScalingDown), delayBeforeShrink); // Esperar antes de escalar hacia abajo.
                    }
                    else
                    {
                        StartScalingDown();
                    }
                }
                else
                {
                    spriteRenderer.enabled = false; // Ocultar el sprite al finalizar el escalado hacia abajo.
                    isScaling = false;
                }
            }
        }

        // Actualizar el sprite si ha cambiado en el Inspector
        if (spriteRenderer.sprite != targetSprite)
        {
            spriteRenderer.sprite = targetSprite;
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
        }
    }

    void StartScalingDown()
    {
        StartScaling(false); // Iniciar escalado hacia abajo (desaparecer).
    }
}
