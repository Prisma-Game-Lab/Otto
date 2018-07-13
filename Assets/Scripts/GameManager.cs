using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameManager instance = null;
    public GameObject UI_WinText;
    public GameObject UI_LoseText;
    private static GameObject MenuCanvas;
    private GameObject hud;
    //public static Vector3 respawn_point = new Vector3(-3.12f, 0.10f, -16.65f);
    public static Vector3 respawn_point = new Vector3(-254.778f, 0.58f, -16.65002f);
    private GameObject plyr;
    public static int lifePoints;
    float delayTime;

    GameObject[] enemyV;
    Vector3[] enemy_chkpt;

    public GameObject Inimigos;
    public GameObject Player;

    public GameObject camera;

    public GameManager getInstance()
    {
        if (instance == null)
            instance = this;

         return instance;
    }

    private void Awake()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        MenuCanvas= GameObject.Find("CanvasMenu");
        plyr = GameObject.Find("Player");
        hud = GameObject.Find("HUD");
        enemyV = GameObject.FindGameObjectsWithTag("Enemy");
        enemy_chkpt = new Vector3[enemyV.Length];

        for (int i=0;i<enemyV.Length;i++){
          enemy_chkpt[i] = enemyV[i].transform.position;
        }

        //UI_WinText = GameObject.Find("VictoryScreen");
        //UI_LoseText = GameObject.Find("DeathScreen");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f){
                Pause();
            }
            else{
                Cursor.visible = false;
                Resume();

            } 
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Win();
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        MenuCanvas.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1f;
       
    }

    void Pause()
    {
        Cursor.visible = true;
        MenuCanvas.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0f;
        
    }

    public void respawn()
    {
        Cursor.visible = true;
        /*lifePoints -= 1;

        if (lifePoints <= 0)
            restart();*/

       // plyr.transform.position = respawn_point;
        /*for (int i = 0; i < enemyV.Length; i++)
        {
            enemyV[i].transform.position = enemy_chkpt[i];
        }*/
        restart();

        if (UI_LoseText.active == true)
            UI_LoseText.SetActive(false);
        if (UI_WinText.active == true)
            UI_WinText.SetActive(false);

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void restart()
    {
        Reset();
    }

	public void Reset()
    {
        MenuCanvas.GetComponent<MainMenu>().changeInstanceGame(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void Win()
    {
        Cursor.visible = true;
        //Debug.Log("Entrou Win");
        UI_WinText.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        Cursor.visible = true;
        UI_LoseText.SetActive(true);
        Time.timeScale = 0f;
    }
}
