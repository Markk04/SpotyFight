using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    private GameObject[] childrenArray;
    private int lifes; // La quantitat de hosties que li pots cardar
    public Color mainColor;
    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Renderer del espectador
        SkinnedMeshRenderer skinnedRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        // Obtener todos los materiales del Renderer
        Material[] materials = skinnedRenderer.materials;
        mainColor = materials[1].color;
        for (int i = 0;i<24; i++){
            OtorgarColores(i,mainColor,false,0);
        }

        lifes=1;
        OtorgarColores(12,new Color(0,0,100),true,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OtorgarColores(int id,Color color,bool enableEmiter,float emiter)
    {
    // Obtener el componente Renderer del espectador
    SkinnedMeshRenderer skinnedRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();

    // Verificar si el componente Renderer existe
    if (skinnedRenderer != null)
    {
        // Obtener todos los materiales del Renderer
        Material[] materials = skinnedRenderer.materials;

        if (id >= 0 && id < materials.Length)
        {
            // Cambiar el color principal
            materials[id].color = color;
            if(enableEmiter){
                // Habilitar la propiedad de emisión
                materials[id].EnableKeyword("_EMISSION");
                // Configurar el color de emisión (puedes ajustarlo según tus necesidades)
                materials[id].SetColor("_EmissionColor", color * emiter); // Multiplicador para intensificar el efecto
            }else{
                // Habilitar la propiedad de emisión
                materials[id].DisableKeyword("_EMISSION");
            }
            
        }
        else
        {
            Debug.LogWarning("ID fuera de rango en la lista de materiales.");
        }
    }
    else
    {
        Debug.LogError("No se encontró un SkinnedMeshRenderer en el objeto.");
    }
    }

}
