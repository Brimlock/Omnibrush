using UnityEngine;
using System.Collections;

public class PanelButton : MonoBehaviour {

    GameObject message;
    // Use this for initialization
    void Start () {
        message = GameObject.Find("Message");
        message.SetActive(false);
	}
	
	// Update is called once per frame
	public void toggleText () {
        message.SetActive(!message.activeInHierarchy);
	}
}
