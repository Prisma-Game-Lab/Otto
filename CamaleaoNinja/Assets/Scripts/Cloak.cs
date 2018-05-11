using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        // entrou na camuflagem
        if (other.tag == "Objeto Interagível")
        {
            // Falta garantir que o player tenha a cor da camuflagem
             gameObject.GetComponentInParent<Player>().cloak();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        // está camuflado
        if (other.tag == "Objeto Interagível")
        {
            gameObject.GetComponentInParent<Player>().cloak();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Saiu da camuflagem
    }

}
