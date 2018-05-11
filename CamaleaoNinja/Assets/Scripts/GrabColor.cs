using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColor : MonoBehaviour {

    // Pensar em uma forma de deixar eles escolherem as cores sem inteferir no código
    // Escolher a cor direto ou escrever um nome? 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Tongue")
        {
            other.GetComponent<Player>().addColor("Verde");
            this.gameObject.SetActive(false);
        }
    }

}
