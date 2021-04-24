using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    GameObject settingsPanel;
	// Use this for initialization
	void Start () {
        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);
	}
	
	public void toggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
