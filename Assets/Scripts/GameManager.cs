using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject UI_WinText;
    public GameObject UI_LoseText;
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
       /* if (Input.GetKeyDown(pause_button))
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                UI_menu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                UI_menu.SetActive(false);
            }*/
 
    }

    public void respawn()
    {

        plyr.transform.position = respawn_point;
        if(UI_LoseText.active == true)
            UI_LoseText.SetActive(false);
        if (UI_WinText.active == true)
            UI_WinText.SetActive(false);
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
