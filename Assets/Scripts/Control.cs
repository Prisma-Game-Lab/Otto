using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    [HideInInspector]
    public bool isOnCredits = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if(!isOnCredits){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<ChangeColor>().SetCamufla(true);

            }


            if (Input.GetKeyUp(KeyCode.Space))
            {
                GetComponent<ChangeColor>().SetCamufla(false);
            }

            if (Input.GetKey(KeyCode.E))
            {
                tongueControl(true);
            }
            else
            {
                tongueControl(false);
            }

            this.GetComponent<Player>().canMove = false;
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
