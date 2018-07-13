using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public GameManager gm;

    private void OnTriggerEnter (Collider other)
    {
        if (other.name == "Player") // Checa se o Collider é o player
            gm.Lose();
    }
}
