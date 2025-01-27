using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverScript : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReloadLevel()
    {
        Debug.Log("Button Pressed");
        SceneManager.LoadScene("XR Spotyfight");
    }
}
