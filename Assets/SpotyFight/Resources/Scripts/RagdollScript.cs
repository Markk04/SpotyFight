using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager");
        Invoke("DestroyMySelf",20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyMySelf(){
        gm.GetComponent<GameManager>().diedEnemy();
        gm.GetComponent<GameManager>().attractionTargetsBlocked.Remove(gameObject);//No ta be Mirar
        Destroy(gameObject);
    }
}
