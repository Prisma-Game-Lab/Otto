using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    //public GameObject MainMenuUI;
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
}
