using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNameCtrl : MonoBehaviour
{
    TextMesh nameText;
    MeshRenderer nameTextRenderer;
    

  

    void Start()
    {
        nameText = GetComponent<TextMesh>();
        nameTextRenderer = GetComponent<MeshRenderer>();
        nameText.text = transform.parent.GetComponent<NPCStat>()._name;
    }

    
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            nameTextRenderer.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nameTextRenderer.enabled = false;
        }
    }

    
}
