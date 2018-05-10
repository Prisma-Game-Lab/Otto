﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space))
        {
            cloakControl();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            tongueControl(true);
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            tongueControl(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            restart(); // será apagado depois que o desenvolvimento terminar, só está aqui para facilitar o Debug
        }

    }

    void cloakControl()
    {
        // Chama a função no player
        Debug.Log("Estou camuflado");
    }

    void tongueControl(bool show)
    {
        // chama a função no player
        gameObject.GetComponent<Player>().tongue(show);
    }

    void pause()
    {
        Debug.Log("Pausa o jogo");
    }

    void restart()
    {
        Debug.Log("Reinicia jogo");
    }

}
