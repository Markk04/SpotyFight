using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManagerScript : MonoBehaviour
{
    // Materiales que se alternarán
    public Material[] camMat;

    // Tiempo en segundos entre cada cambio
    public float changeInterval = 2f;

    private MeshRenderer meshRenderer;
    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        // Obtenemos el MeshRenderer del objeto
        meshRenderer = GetComponent<MeshRenderer>();

        // Asegurarnos de que el primer material se aplique al inicio
        if (camMat.Length > 0)
        {
            meshRenderer.material = camMat[currentIndex];
        }
    }

    void Update()
    {
        // Incrementamos el temporizador
        timer += Time.deltaTime;

        // Si el tiempo supera el intervalo, cambiamos de material
        if (timer >= changeInterval)
        {
            // Reiniciamos el temporizador
            timer = 0f;

            // Cambiamos al siguiente material
            currentIndex = (currentIndex + 1) % camMat.Length;

            // Aplicamos el nuevo material
            meshRenderer.material = camMat[currentIndex];
        }
    }
}
