using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorAnimationScript : MonoBehaviour
{
    private string[] animationTrigger = new string[] { "FarrukoTrigger", "AupaTrigger" };
    private List<GameObject> spectatorsList;
    private Dictionary<GameObject, float> animationDelays;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa la lista con todos los objetos que tienen el tag "Spectator"
        spectatorsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spectator"));

        // Diccionario para almacenar tiempos de retraso individuales
        animationDelays = new Dictionary<GameObject, float>();

        // Configurar retrasos iniciales aleatorios para cada espectador
        foreach (GameObject spectator in spectatorsList)
        {
            animationDelays[spectator] = Random.Range(0f, 3f); // Retraso inicial aleatorio entre 0 y 3 segundos
            //Debug.Log("Espectador encontrado: " + spectator.name + " - Retraso inicial: " + animationDelays[spectator]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject spectator in spectatorsList)
        {
            Animator animator = spectator.GetComponentInChildren<Animator>();
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Reducir el retraso de este espectador
            animationDelays[spectator] -= Time.deltaTime;

            // Si el retraso ha llegado a 0 y la animación actual ha terminado
            if (animationDelays[spectator] <= 0f && stateInfo.IsName("Base_1") && stateInfo.normalizedTime >= 1.0f)
            {
                // Selecciona un índice aleatorio del arreglo animationTrigger
                int randomIndex = Random.Range(0, animationTrigger.Length);
                string randomTrigger = animationTrigger[randomIndex];

                // Activa el trigger aleatorio
                Debug.Log(spectator.name + " - Trigger activado: " + randomTrigger);
                animator.SetTrigger(randomTrigger);

                // Asigna un nuevo retraso aleatorio antes de la próxima animación
                animationDelays[spectator] = Random.Range(1f, 5f); // Retrasos entre 1 y 5 segundos
            }
        }
    }
}
