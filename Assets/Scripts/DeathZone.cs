﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.Lose();
        Destroy(other.gameObject);
    }
}
