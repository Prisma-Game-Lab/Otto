using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    // pegar pela tag os inimigos

    public GameManager gm;

    public GameObject playerawake;
    public GameObject inimigos;

    public GameObject Camera;

    [HideInInspector]
    public static bool iniciouGame;

    public void changeInstanceGame(bool state){
        if (state)
        {
            if (!iniciouGame)
            {
                iniciouGame = state;
            }
        }
    }

    void Start()
    {
        if(iniciouGame){
            this.gameObject.SetActive(false);
            Cursor.visible = false;
            Camera.GetComponent<Animator>().enabled = false;
            Camera.GetComponent<Camera>().enabled = true;
            Time.timeScale = 1f;
            /*playerawake.SetActive(true);
            foreach (Transform child in inimigos.GetComponentInChildren<Transform>())
            {
                child.gameObject.SetActive(true);
            }*/

        } else {
            Cursor.visible = true;

        }
        //print(iniciouGame + " INICIOU");
        //  Time.timeScale = 0f;
        //Cursor.visible = true;
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
        print("Inicia jogo");
        Cursor.visible = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);

        playerawake.SetActive(true);

        foreach(Transform child in inimigos.GetComponentInChildren<Transform>()){
            child.gameObject.SetActive(true);
        }

        Camera.GetComponent<Animator>().SetTrigger("StartGame");
    }

   /* public void ResetGame(){

        print("Reinicia jogo");


        Cursor.visible = false;
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);

        playerawake.SetActive(true);

        foreach (Transform child in inimigos.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }

        //StartGame.SetTrigger("StartGame");
    }*/

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Restart()
    {
        gm.respawn();
    }
}
