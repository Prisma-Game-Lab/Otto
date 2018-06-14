using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject UI_WinText;
    public GameObject UI_LoseText;
    public GameObject MenuCanvas;
    public static Vector3 respawn_point = new Vector3(-3.12f, 0.11f, -16.65f);
    private GameObject plyr;
    public static int lifePoints;
    float delayTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        plyr = GameObject.FindWithTag("Player");
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
        MenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        MenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void respawn()
    {

        plyr.transform.position = respawn_point;
         
        if(UI_LoseText.active == true)
            UI_LoseText.SetActive(false);
        if (UI_WinText.active == true)
            UI_WinText.SetActive(false);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Win()
    {
        //Debug.Log("Entrou Win");
        UI_WinText.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        UI_LoseText.SetActive(true);
        Time.timeScale = 0;
        //respawn();
    }
}
