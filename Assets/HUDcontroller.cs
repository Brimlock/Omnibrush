using UnityEngine;
using System.Collections;

public class HUDcontroller : MonoBehaviour {
    GameObject pauseMenu;
	// Use this for initialization
	void Start () {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) togglePause();
	}
    public void togglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        if (pauseMenu.activeInHierarchy) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
