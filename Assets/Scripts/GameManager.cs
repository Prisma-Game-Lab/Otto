﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject UWinText;
    public GameObject ULoseText;
    public float delayTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void respawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Win()
    {
        UWinText.SetActive(true);
        //Invoke("respawn", delayTime);
    }

    public void Lose()
    {
        ULoseText.SetActive(true);
        //Invoke("respawn", delayTime);
    }
}
