using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject UI_WinText;
    public GameObject UI_LoseText;
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
    }

    // Use this for initialization
    void Start () {
		
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
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Win()
    {
        Debug.Log("Entrou Win");
        UI_WinText.SetActive(true);
        Time.timeScale = 0;
        //respawn();
    }

    public void Lose()
    {
        Debug.Log("Entrou Lose");
        UI_LoseText.SetActive(true);
        Time.timeScale = 0;
        //respawn();
    }
}
