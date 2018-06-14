using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu1 : MonoBehaviour {

    public static bool onThisMenu = false;

    public GameObject pauseMenuUI;
    public GameObject MainMenuUI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
                Pause();
            else Resume();
        }
	}
    public void Resume()
    {
        onThisMenu = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

    }

    void Pause()
    {
        onThisMenu = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {
        onThisMenu = false;
        pauseMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    public void Restart()
    {
        GameManager.instance.respawn();
    }
}
