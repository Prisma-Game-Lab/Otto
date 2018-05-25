using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryPoint : MonoBehaviour {
    void OnTriggerEnter()
    {
        GameManager.instance.Win();
    }
}
