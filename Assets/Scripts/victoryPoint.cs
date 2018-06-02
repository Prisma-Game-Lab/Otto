using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryPoint : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player") // Checa se o Collider é o player
            GameManager.instance.Win();

        //Debug.Log(other.name);
    }
}
