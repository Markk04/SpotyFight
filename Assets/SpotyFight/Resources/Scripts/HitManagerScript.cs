using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    private GameObject[] childrenArray;
    private int lifes; // La quantitat de hosties que li pots cardar
    public Color mainColor;
    private List<int> idsToHit;
    private BoxCollider boxCollider;
    private Rigidbody mannequinRb;  // Rigidbody of the mannequin.
    private bool triggerFinalHit;
    public GameObject maniquiRagdoll;
    public GameObject gm;
    private GameObject target;
    private AudioSource audioSource;
    private AudioClip[] audioList;
    

    // Lista de ids
    public List<int> originalList = new List<int> { 0,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23 }; // Falta el 16 que amb el desktop no arribo

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Renderer del espectador
        gm = GameObject.FindGameObjectWithTag("GameManager");
        audioSource = gm.GetComponent<AudioSource>();
        audioList = gm.GetComponent<GameManager>().GetAudioList();
        SkinnedMeshRenderer skinnedRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        boxCollider = GetComponentInParent<BoxCollider>();
        mannequinRb = GetComponentInParent<Rigidbody>();
        // Obtener todos los materiales del Renderer
        Material[] materials = skinnedRenderer.materials;
        mainColor = materials[1].color;
        for (int i = 0;i<24; i++){
            OtorgarColores(i,mainColor,false,0);
        }

        lifes=3;

        idsToHit = GetRandomizedList(originalList);

        
        //OtorgarColores(0,new Color(0,0,100),true,10);
        //OtorgarColores(2,new Color(100,0,0),true,10);
        //OtorgarColores(3,new Color(0,0,100),true,10);
        //OtorgarColores(4,new Color(0,100,0),true,10);
        //OtorgarColores(5,new Color(100,0,0),true,10);
        //OtorgarColores(6,new Color(0,0,100),true,10);
        //OtorgarColores(7,new Color(0,100,0),true,10);
        //OtorgarColores(8,new Color(100,0,0),true,10);
        //OtorgarColores(9,new Color(0,0,100),true,10);
        //OtorgarColores(10,new Color(0,0,100),true,10);
        //OtorgarColores(11,new Color(0,100,0),true,10);
        //OtorgarColores(12,new Color(100,0,0),true,10);
        //OtorgarColores(13,new Color(0,0,100),true,10);
        //OtorgarColores(14,new Color(0,100,0),true,10);
        //OtorgarColores(15,new Color(100,0,0),true,10);
        //OtorgarColores(16,new Color(0,0,100),true,10);
        //OtorgarColores(17,new Color(0,100,0),true,10);
        //OtorgarColores(18,new Color(100,0,0),true,10);
        //OtorgarColores(19,new Color(0,0,100),true,10);
        //OtorgarColores(20,new Color(0,100,0),true,10);
        //OtorgarColores(21,new Color(100,0,0),true,10);
        //OtorgarColores(22,new Color(0,0,100),true,10);
        transform.LookAt(Vector3.zero);

        triggerFinalHit = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(idsToHit.Count >0){
            Debug.Log(idsToHit[0]);
        }
        if (idsToHit.Count <= 0 && triggerFinalHit)
        {
            Debug.Log("A tomar por culo");
            //DisableChildObjectsWithTag("BoxColiderManager");
            //// Incrementar la altura del BoxCollider
            //Vector3 newSize = boxCollider.size;
            //newSize.y += 5f; // Incrementar altura (eje Y)
            //boxCollider.size = newSize;
            //Vector3 newCenter = boxCollider.center;
            //newCenter.y += 2.5f;
            //boxCollider.center = newCenter;
            ////Acordarse del centro tambien sino no va
            //mannequinRb.useGravity = true;
            //mannequinRb.isKinematic = false;
            //triggerFinalHit=false;
            //Invoke("DestroyMySelf",10);
            gm.GetComponent<GameManager>().sumScore(2);
            passarARagdoll();
        } else if (idsToHit.Count == 1){
            OtorgarColores(idsToHit[0], new Color(100, 0, 0), true, 10);
        }
        else
        {
            if(idsToHit.Count >0){
                OtorgarColores(idsToHit[0], new Color(0, 100, 0), true, 10);
            }
        }
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

    // Método para obtener una lista aleatoria
    List<int> GetRandomizedList(List<int> inputList)
    {
    List<int> tempList = new List<int>(inputList); // Copia de la lista original
    List<int> resultList = new List<int>();
    int index = 0;
    while (index < lifes)
    {
        // Elegir un índice aleatorio
        int randomIndex = Random.Range(0, tempList.Count);

        // Agregar el elemento a la nueva lista
        resultList.Add(tempList[randomIndex]);

        // Remover el elemento de la lista temporal
        tempList.RemoveAt(randomIndex);
        index++;
    }
    return resultList;
}


    public bool isHitted(int id)
    {
        if (idsToHit.Count > 0 && id == idsToHit[0])
        {
            idsToHit.RemoveAt(0); // Elimina la posición 0
            int randomNum = Random.Range(1, audioList.Length);
            if(randomNum == 0){randomNum = 1;}
            audioSource.PlayOneShot(audioList[randomNum]);
            return true;
        }else{
            return false;
        }
    }

    void DisableChildObjectsWithTag(string tag)
    {
        // Obtiene todos los hijos del objeto actual
        Transform parent = transform.parent;

        // Recorre todos los transform encontrados
        foreach (Transform child in parent)
        {
            Debug.Log(child.tag);
            // Verifica si el hijo tiene el tag especificado
            if (child.CompareTag(tag))
            {
                // Desactiva el GameObject del hijo
                child.gameObject.SetActive(false);
                Debug.Log($"Desactivado: {child.name}");
            }
        }
    }

    void DestroyMySelf(){
        Destroy(transform.parent.gameObject);
    }

    public void setTarget(GameObject target){
        this.target = target;
    }

    public void passarARagdoll()
    {
        Instantiate(maniquiRagdoll, gameObject.transform.position, Quaternion.identity);
        gm.GetComponent<GameManager>().diedEnemy(target);
        DestroyMySelf();
    }

}
