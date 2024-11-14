using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagementScript : MonoBehaviour
{

    private int playerScore;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScore=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SumScore(int num){
        playerScore+=num;
    }

    public void RestScore(int num){
        playerScore-=num;
    }


}
