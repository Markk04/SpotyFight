using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rotationObject : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Transform transform= cam.transform;
        transform.LookAt(transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
