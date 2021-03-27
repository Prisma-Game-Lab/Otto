using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameManager instance = null;
    public GameObject UI_WinText;
    public GameObject UI_LoseText;
    public GameObject UI_Final;

    private static GameObject MenuCanvas;
    private GameObject hud;
    //public static Vector3 respawn_point = new Vector3(-3.12f, 0.10f, -16.65f);
    public static Vector3 respawn_point = new Vector3(-254.778f, 0.58f, -16.65002f);
    public static int lifePoints;
    float delayTime;

    GameObject[] enemyV;
    Vector3[] enemy_chkpt;

    private GameObject Inimigos;

    public GameManager getInstance()
    {
        if (instance == null)
            instance = this;

         return instance;
    }

    public void IniciaGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void Awake()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        MenuCanvas= GameObject.Find("CanvasMenu");
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
        Time.timeScale = 0f;
        
    }

    public void respawn()
    {
        Cursor.visible = true;
        
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

        // Desativa inimigos
        GameObject[] enm = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject child in enm){
            child.SetActive(false);
        }

        UI_Final.SetActive(true);

        // Ativa a animacao de final, aparece o cubo enorme branco,
        // aparece a animação, dentro da animacao ele não pode apertar nada,
        // se ele apertar qualquer botão ele volta pro menu - playGame()

        // inicia a animacao do final e 
        // UI_Final
    }

    public void Lose()
    {
        //respawn();
        UI_LoseText.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
