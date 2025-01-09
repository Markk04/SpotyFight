using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de que usas este espacio de nombres si necesitas mostrar el tiempo en pantalla

public class GameManager : MonoBehaviour
{
    private GameObject spectatorManager;
    private GameObject enemyGenerator;
    private GameObject scoreManager;
    private int seconds;
    private int minutes;
    private float timer;
    public GameObject[] attractionTargets; // Array de objetos a los que se puede atraer
    private Gameobject[] attractionTargetsBlocked; // Array de objetos a los que se puede atraer
    public int gamePhase = 1; // 0: Inicio, 1: Un enemig, 2: Dos enemigs, 3: Tres enemigs, 4: Quatre enemigs... -1: Fin
    private int maxEnemies = 1;
    private int enemiesSpawned = 0;

    //public Text timerText;

    void Start()
    {
        spectatorManager = GameObject.FindGameObjectWithTag("ScoreManagement");
        enemyGenerator = GameObject.FindGameObjectWithTag("ManiquinGeneratorTag");
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManagement");
        
        seconds = 0;
        minutes = 0;
        timer = 0f;

        // Opcional: inicializa el texto si tienes un elemento UI
        //if (timerText != null)
        //{
        //    timerText.text = "00:00";
        //}
    }

    void Update()
    {
        if(gamePhase>0){
            // Incrementa el temporizador basado en el tiempo real transcurrido
            timer += Time.deltaTime;

            if (timer >= 1f) // Cada segundo
            {
                timer = 0f; // Reinicia el temporizador para el siguiente segundo
                seconds++;

                //Spawn any enemy
                if(enemiesSpawned < maxEnemies){
                    GameObject targetSelected = attractionTargets[Random.Range(0, attractionTargets.Length)];
                    while(targetSelected in attractionTargetsBlocked){
                        targetSelected = attractionTargets[Random.Range(0, attractionTargets.Length)];
                    }
                    enemyGenerator.GetComponent<ManiquinCorreCorreGenScript>().enemyGenerator(targetSelected);
                    enemiesSpawned++;
                }

                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;
                }

                // Actualiza el texto del contador en pantalla, si es necesario
                //if (timerText != null)
                //{
                //    timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
                //}
            }
        }

        switch(gamePhase){
            case 1:
            maxEnemies = 1;
            if(scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 10){
                gamePhase = 2;
            }
                break;
            case 2:
            maxEnemies = 2;
            if(scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 20){
                gamePhase = 3;
            }
                break;
            case 3:
            maxEnemies = 3;
            if(scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 40){
                gamePhase = 4;
            }
                break;
            case 4:
            maxEnemies = 4;
            if(scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 80){
                gamePhase = 5;
            }
                break;
            case 5:
            maxEnemies = 5;
            if(scoreManager.GetComponent<ScoreManagementScript>().playerScore >= 160){
                gamePhase = 6;
            }
                break;
            case 6:
            maxEnemies = 6;
                break;
            case -1:
                break;
        }
        

    }

    public void sumScore(int n)
    {
        spectatorManager.GetComponent<ScoreManagementScript>().SumScore(n);
    }

    public void setGamePhase(int phase)
    {
        gamePhase = phase;
    }

    public void diedEnemy()
    {
        enemiesSpawned--;
    }

    public GameObject[] getAttractionTargetsBlocked()
    {
        return attractionTargetsBlocked;
    }


}
