using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GameObject spectatorManager;
    private GameObject enemyGenerator;
    private GameObject scoreManager;
    private GameObject girlPrefab;
    public GameObject girl;
    private int seconds;
    private int minutes;
    private float timer;
    private GameObject[] attractionTargets; // Array de objetos a los que se puede atraer
    private HashSet<GameObject> attractionTargetsBlocked; // HashSet para objetivos bloqueados
    public int gamePhase = 0; // 0: Inicio, 1: Un enemigo, 2: Dos enemigos, etc. -1: Fin
    private int maxEnemies = 1;
    private int enemiesSpawned = 0;
    public GameObject[] lights;
    public GameObject ringLight;

    void Start()
    {
        spectatorManager = GameObject.FindGameObjectWithTag("ScoreManagement");
        enemyGenerator = GameObject.FindGameObjectWithTag("ManiquinGeneratorTag");
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManagement");
        attractionTargets = GameObject.FindGameObjectsWithTag("attractionTarget");
        lights = GameObject.FindGameObjectsWithTag("Light");

        showGirl("Golpeja al maniqui per començar el joc. Axel maricon");

        if (spectatorManager == null || enemyGenerator == null || scoreManager == null)
        {
            Debug.LogError("Uno o más objetos no están asignados. Verifica las etiquetas o asignaciones.");
        }

        seconds = 0;
        minutes = 0;
        timer = 0f;

        // Inicializa el conjunto de objetivos bloqueados
        attractionTargetsBlocked = new HashSet<GameObject>();

        ringLight.SetActive(false);
    }

    void Update()
    {
        if (gamePhase > 0)
        {
            // Incrementa el temporizador basado en el tiempo real transcurrido
            timer += Time.deltaTime;

            if (timer >= 1f) // Cada segundo
            {
                timer = 0f; // Reinicia el temporizador para el siguiente segundo
                seconds++;

                // Genera un enemigo si es posible
                if (enemiesSpawned < maxEnemies)
                {
                    List<GameObject> validTargets = new List<GameObject>(attractionTargets);
                    validTargets.RemoveAll(target => attractionTargetsBlocked.Contains(target));

                    if (validTargets.Count > 0)
                    {
                        GameObject targetSelected = validTargets[Random.Range(0, validTargets.Count)];
                        enemyGenerator.GetComponent<ManiquinCorreCorreGenScript>().enemyGenerator(targetSelected);
                        enemiesSpawned++;
                        attractionTargetsBlocked.Add(targetSelected);
                    }
                    else
                    {
                        Debug.LogWarning("No hay objetivos válidos para generar enemigos.");
                    }
                }

                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;
                }
            }
        }

        // Actualiza la lógica de fases del juego
        switch (gamePhase)
        {
            case 1:
                maxEnemies = 1;
                if (scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 10)
                {
                    gamePhase = 2;

                }
                break;
            case 2:
                maxEnemies = 2;
                if (scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 20)
                {
                    gamePhase = 3;
                }
                break;
            case 3:
                maxEnemies = 3;
                if (scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 40)
                {
                    gamePhase = 4;
                }
                break;
            case 4:
                maxEnemies = 4;
                if (scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 80)
                {
                    gamePhase = 5;
                }
                break;
            case 5:
                maxEnemies = 5;
                if (scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 160)
                {
                    gamePhase = 6;
                }
                break;
            case 6:
                maxEnemies = 6;
                break;
            case -1:
                // Fin del juego
                break;
        }
    }

    public void sumScore(int n)
    {
        if (spectatorManager != null)
        {
            spectatorManager.GetComponent<ScoreManagementScript>().SumScore(n);
        }
    }

    public void setGamePhase(int phase)
    {
        gamePhase = phase;
    }

    public void diedEnemy(GameObject target)
    {
        enemiesSpawned--;
        // Desbloquea el objetivo asociado con el enemigo destruido
        if (attractionTargetsBlocked.Contains(target))
        {
            attractionTargetsBlocked.Remove(target);
        }
        else
        {
            Debug.LogWarning("El objetivo a desbloquear no estaba bloqueado.");
        }
    }

    public HashSet<GameObject> getAttractionTargetsBlocked()
    {
        return attractionTargetsBlocked;
    }

    public void startGame(){
        gamePhase = 1;
    }

    public void OnScoreColliderEnter(){
        sumScore(5);
        Debug.Log("Las tirao mu bien");
    }

    public void showGirl(String msg){
        girlPrefab = Instantiate(girl, transform.position, transform.rotation);
        girlPrefab.GetComponent<girlSquirt>().setText("TU RECONTRAPUTISSIMA MADRE");
    }

    public void hideGirl(){
        Destroy(girlPrefab);
    }

    public void turnOffTheLights(){
        Debug.Log("Apagando luces");
        Debug.Log(lights.Length);
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
            Debug.Log("Apagando luz");
            Debug.Log(light.name);
        }
    }

    public void turnOnTheLights(){
        Debug.Log("Encendiendo luces");
        Debug.Log(lights.Length);
        foreach (GameObject light in lights)
        {
            if (light == null){
                Debug.Log("La luz es nula");
            }
            light.SetActive(true);
            Debug.Log("Encendiendo luz");
            Debug.Log(light.name);
        }
    }

}
