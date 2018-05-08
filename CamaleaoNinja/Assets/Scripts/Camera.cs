using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject Player;

    private Vector3 _offset;


    // Use this for initialization
    void Start () {
      _offset = transform.position - Player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        followPlayer();
	}

    private void followPlayer()
    {
        print(Player.transform.position);
        this.transform.position = Player.transform.position + _offset;
    }

}
