using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    void Start()
    {
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Awake()
    {
        Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Restart()
    {
        GameManager.instance.respawn();
    }
}
