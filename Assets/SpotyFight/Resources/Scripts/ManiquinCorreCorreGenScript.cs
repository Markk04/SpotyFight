using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiquinCorreCorreGenScript : MonoBehaviour
{
    public GameObject maniquinCorreCorrePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Transform childTransform = transform.Find("GenPosition");
        if (childTransform != null)
        {
            // Instanciar el prefab en la posición y rotación del hijo
            Instantiate(maniquinCorreCorrePrefab, childTransform.position, childTransform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
