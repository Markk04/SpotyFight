using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagementScript : MonoBehaviour
{
    public int playerScore;
    private int playerScoreAntes;
    private GameObject[] spectators; // Lista de objetos con el tag "Spectator"
    public int scoreThreshold = 10; // Puntaje necesario para mostrar un espectador
    private Color[] skinTones = new Color[]
        {
            new Color(0.9f, 0.7f, 0.5f), // Tonalidad clara (tono de piel muy clara)
            new Color(0.8f, 0.6f, 0.4f), // Tonalidad clara
            new Color(0.7f, 0.5f, 0.3f), // Tonalidad media-clara
            new Color(0.6f, 0.4f, 0.2f), // Tonalidad media
            new Color(0.5f, 0.3f, 0.1f), // Tonalidad oscura
            new Color(0.4f, 0.2f, 0.1f)  // Tonalidad muy oscura (tono de piel muy oscuro)
        };
     private Color[] hairColor = new Color[]
        {
            new Color(0.85f, 0.35f, 0.18f), // Pelirrojo (rojo intenso)
            new Color(0.9f, 0.75f, 0.45f), // Rubio claro (rubio muy claro)
            new Color(0.6f, 0.5f, 0.4f), // Rubio oscuro (rubio más oscuro, con tono marrón)
            new Color(0.1f, 0.1f, 0.1f), // Negro (cabello negro)
            new Color(0.35f, 0.25f, 0.2f), // Moreno claro (marrón claro)
            new Color(0.2f, 0.1f, 0.05f), // Moreno oscuro (marrón muy oscuro)
        };

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        playerScoreAntes = playerScore;

        // Buscar todos los GameObjects con el tag "Spectator"
        spectators = GameObject.FindGameObjectsWithTag("Spectator");
        Mezclar(spectators);
        // Hacer invisibles todos los espectadores al inicio
        foreach (var spectator in spectators)
        {
            OtorgarColores(spectator); // Asignar color aleatorio a cada espectador
            spectator.SetActive(false); // Desactivar los espectadores al inicio
        }
        // Llamamos a la función para actualizar la visibilidad de los espectadores al inicio
        UpdateSpectatorsVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        // Llamar para asegurarse de que los espectadores se actualicen cuando el puntaje cambie
        if (playerScore != playerScoreAntes)
        {
            UpdateSpectatorsVisibility();
            playerScoreAntes = playerScore;
        }
    }

    // Método para agregar puntos
    public void SumScore(int num)
    {
        playerScore += num;
        UpdateSpectatorsVisibility();  // Actualizar la visibilidad de los espectadores al sumar puntos
    }

    // Método para restar puntos
    public void RestScore(int num)
    {
        playerScore -= num;
        UpdateSpectatorsVisibility();  // Actualizar la visibilidad de los espectadores al restar puntos
    }

    // Método para actualizar la visibilidad de los espectadores
    private void UpdateSpectatorsVisibility()
    {
        // Activar espectadores según el puntaje
        for (int i = 0; i < spectators.Length; i++)
        {
            if (playerScore >= (i + 1) * scoreThreshold)
            {
                spectators[i].SetActive(true);  // Activar el espectador si el puntaje es suficiente
            }
            else
            {
                spectators[i].SetActive(false);  // Desactivar el espectador si el puntaje no lo alcanza
            }
        }
    }

    // Baraja una lista de manera aleatoria
    void Mezclar(GameObject[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = Random.Range(i, n);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Método para otorgar un color aleatorio a los materiales del espectador según su shader
    void OtorgarColores(GameObject spectator)
    {
        // Obtener el componente Renderer del espectador
        Renderer skinnedrenderer = spectator.GetComponentInChildren<SkinnedMeshRenderer>();

        // Verificar si el componente Renderer existe
        if (skinnedrenderer != null)
        {
            // Obtener todos los materiales del Renderer
            Material[] materials = skinnedrenderer.materials;
            materials[0].color = skinTones[Random.Range(0, skinTones.Length)];
            materials[1].color = new Color(Random.value, Random.value, Random.value);
            materials[2].color = hairColor[Random.Range(0, hairColor.Length)];
        }
        else
        {
            Debug.LogWarning("El espectador " + spectator.name + " no tiene un componente Renderer.");
        }
    }
}
