using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteragibleObjects : Objects {

    public bool Fixed;
    public float SpeedObject = 5f;
    public float SpeedPlayer = 5f;

    // Use this for initialization
    void Start () {
		if (Fixed == true)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*public void pullPlayer(Transform player)
    {
        // fazer ele parar de puxar quando tocar no player
        player.transform.position = Vector3.MoveTowards(player.position, transform.position, SpeedPlayer * Time.deltaTime);
    }*/

    public void pullObject(Transform player)
    {
        // Fazer parar quando tocar no player
        transform.position = Vector3.MoveTowards(transform.position, player.position, SpeedObject * Time.deltaTime);
    }
}
