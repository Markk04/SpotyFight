using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMySelf",10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyMySelf(){
        Destroy(gameObject);
    }
}
