using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryPoint : MonoBehaviour {

    public GameManager gm;

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player") // Checa se o Collider é o player
            gm.Win();
    }
}
