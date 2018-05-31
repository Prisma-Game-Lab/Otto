using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter (Collider other)
    {
        Destroy(other.gameObject);
        //GameManager.instance.Lose();
<<<<<<< HEAD
        GameManager.instance.respawn();
=======
>>>>>>> 109b601e0819d12c13bfd9e6d21662e4d04d4822
    }
}
