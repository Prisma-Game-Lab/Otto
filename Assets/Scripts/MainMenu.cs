using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject playerawake;
    public GameObject enemy01;
    public GameObject enemy02;
    public GameObject enemy03;
    public GameObject enemy04;
    public GameObject enemy05;
    public GameObject enemy06;
    public GameObject enemy07;
    public GameObject enemy08;
    public Animator StartGame;



    void Start()
    {
      //  Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Awake()
    {
     //   Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        Debug.Log("Comecou");

        playerawake.SetActive(true);
        enemy01.SetActive(true);
        enemy02.SetActive(true);
        enemy03.SetActive(true);
        enemy04.SetActive(true);
        enemy05.SetActive(true);
        enemy06.SetActive(true);
        enemy07.SetActive(true);
        enemy08.SetActive(true);
        StartGame.SetTrigger("StartGame");


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
