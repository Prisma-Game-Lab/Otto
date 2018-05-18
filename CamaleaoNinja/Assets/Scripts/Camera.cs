using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject Player;
    public Vector3 distanciaPlayer = new Vector3(0, 15, 20);


    // Use this for initialization
    void Start () {
        this.transform.position = Player.transform.position + distanciaPlayer;
    }

    // Update is called once per frame
    void Update () {
        followPlayer();
	}

    private void followPlayer()
    {
        this.transform.position = Player.transform.position + distanciaPlayer;
    }

}
