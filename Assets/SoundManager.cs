using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public Transform pT;
    public GameObject Luiz;
    public GameObject Gabriel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(pT.position.z>0)
        {
            Gabriel.SetActive(true);
            Luiz.SetActive(false);
        }
        else
            Gabriel.SetActive(false);
        Luiz.SetActive(true);
	}
}
