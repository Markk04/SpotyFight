using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorAnimationScript : MonoBehaviour
{

    public string[] animationTrigger = new string[] { "Aplause","Aupa","Base1","Dab","Fight","Punch" };
    private List<GameObject> spectatorsList;

    // Start is called before the first frame update
    void Start()
    {
         // Inicializa la lista con todos los objetos que tienen el tag "Spectator"
        spectatorsList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spectator"));

        // Opcional: Verificar la cantidad de objetos encontrados y listarlos en la consola
        Debug.Log("Número de espectadores encontrados: " + spectatorsList.Count);
        foreach (GameObject spectator in spectatorsList)
        {
            Debug.Log("Espectador encontrado: " + spectator.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject spectator in spectatorsList){
            AnimatorStateInfo stateInfo = spectator.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("Base_1")){
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Animator an = spectator.GetComponentInChildren<Animator>();
                    an.SetTrigger("AupaTrigger");
                    Debug.Log("MuevetePuto");
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Animator an = spectator.GetComponentInChildren<Animator>();
                    an.SetTrigger("FarrukoTrigger");
                    Debug.Log("MuevetePuto");
                }
            }
            if(!stateInfo.IsName("Base_1")){
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Animator an = spectator.GetComponentInChildren<Animator>();
                    an.SetTrigger("BaseTrigger");
                    Debug.Log("QuietoPuto");
                }
            }
        }
        
    }
}
