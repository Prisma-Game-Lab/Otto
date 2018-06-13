using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("soltei espaco");
            GetComponent<ChangeColor>().SetCamufla(true);
            
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            print("soltei espaco");
            GetComponent<ChangeColor>().SetCamufla(false);
        }

        if (Input.GetKey(KeyCode.Q) || Input.GetMouseButton(0))
        {
            tongueControl(true);
        } else
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
