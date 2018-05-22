using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubinho : MonoBehaviour {

    public Animator playerAnim;

	// Use this for initialization
	void Start () {
        playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
        {
            playerAnim.SetTrigger("pula");

        }
	}
}
