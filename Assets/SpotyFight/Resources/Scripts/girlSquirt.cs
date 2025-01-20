using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class girlSquirt : MonoBehaviour
{
    public TextMeshPro textGirl;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(String msg)
    {
        textGirl.text = msg;
    }
}
