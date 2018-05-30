using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour {

    public Transform playertransform;
    public GameObject pre;
    public GameObject post;

	// Update is called once per frame
	void Update () {
		if(playertransform.position.y<0)
        {
            pre.SetActive(false);
            post.SetActive(true);
        }
	}
}
