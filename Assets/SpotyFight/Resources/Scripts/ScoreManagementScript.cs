using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagementScript : MonoBehaviour
{
    public int playerScore;
    private int playerScoreAntes;
    private GameObject[] spectators; // Lista de objetos con el tag "Spectator"
    public int scoreThreshold = 10; // Puntaje necesario para mostrar un espectador
    private int currentThresholdIndex = 0; // Índice para saber qué espectador debe aparecer

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
            spectator.SetActive(false);
        }
        // Llamamos a la función para actualizar la visibilidad de los espectadores al inicio
        UpdateSpectatorsVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        // Llamar para asegurarse de que los espectadores se actualicen cuando el puntaje cambie
        if(playerScore!=playerScoreAntes){
            UpdateSpectatorsVisibility();
            playerScoreAntes=playerScore;
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

 //Barreja una llista de manera al random
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

}
