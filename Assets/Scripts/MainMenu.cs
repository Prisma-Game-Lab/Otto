using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    // pegar pela tag os inimigos

    public GameObject playerawake;
    public GameObject inimigos;

    public Animator StartGame;



    void Start()
    {
        //  Time.timeScale = 0f;
        Cursor.visible = true;
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

        playerawake.SetActive(true);

        foreach(Transform child in inimigos.GetComponentInChildren<Transform>()){
            child.gameObject.SetActive(true);
        }

        StartGame.SetTrigger("StartGame");
    }

    public void ResetGame(){
        
        Cursor.visible = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);

        playerawake.SetActive(true);

        foreach (Transform child in inimigos.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }

        //StartGame.SetTrigger("StartGame");
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
