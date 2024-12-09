using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    private GameObject[] childrenArray;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject != null)
        {
            int childCount = gameObject.transform.childCount;
            childrenArray = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                childrenArray[i] = gameObject.transform.GetChild(i).gameObject;
            }

            // Ejemplo: Imprime los nombres de los hijos
            foreach (var child in childrenArray)
            {
                Debug.Log(child.name);
            }
        }
        else
        {
            Debug.LogError("Parent object is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
