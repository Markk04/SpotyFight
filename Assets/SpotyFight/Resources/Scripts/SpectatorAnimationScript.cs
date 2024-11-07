using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorAnimationScript : MonoBehaviour
{

    public string[] animationTrigger = new string[] { "Aplause","Aupa","Base1","Dab","Fight","Punch" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Base_1")){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Animator an = GetComponent<Animator>();
                an.SetTrigger("AupaTrigger");
                Debug.Log("MuevetePuto");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Animator an = GetComponent<Animator>();
                an.SetTrigger("FarrukoTrigger");
                Debug.Log("MuevetePuto");
            }
        }
        if(!stateInfo.IsName("Base_1")){
             if (Input.GetKeyDown(KeyCode.Space))
            {
                Animator an = GetComponent<Animator>();
                an.SetTrigger("BaseTrigger");
                Debug.Log("QuietoPuto");
            }
        }
    }
}
