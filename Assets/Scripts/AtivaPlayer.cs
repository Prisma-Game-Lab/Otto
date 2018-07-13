using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaPlayer : MonoBehaviour {

    public GameObject player;
    public GameObject Inimigos;

	// Use this for initialization
	void Start () {
		
        player.SetActive(true);

        foreach (Transform child in Inimigos.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
