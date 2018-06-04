using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter (Collider other)
    {
        if (other.name == "Player") // Checa se o Collider é o player
            GameManager.instance.Lose();
    }
}
