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
        Transform transforma = cam.transform;

        // Asigna la rotación mirando hacia el transform de la cámara
        transform.LookAt(transforma);

        // Obtén la rotación actual
        Vector3 rotation = transform.eulerAngles;

        // Cambia la componente Y a 0
        rotation.x = 0;

        // Asigna de nuevo la rotación
        transform.rotation = Quaternion.Euler(rotation);
        
        // Debug.Log("Girando hacia " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Aquí puedes mantener el comportamiento o añadir lógica adicional si es necesario
    }
}
