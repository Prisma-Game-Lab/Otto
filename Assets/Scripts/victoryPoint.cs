﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryPoint : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.Win();
        Destroy(other.gameObject);
    }
}
