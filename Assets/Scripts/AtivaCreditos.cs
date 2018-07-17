using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaCreditos : MonoBehaviour {

    public Control controle;
    public GameManager gm;

	// Use this for initialization
	void Start () {

        controle.isOnCredits = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown)
        { 
            gm.IniciaGame();
        }

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Branco")){
            gm.IniciaGame();
        }

    }
}
