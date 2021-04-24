using UnityEngine;
using System.Collections;

public class fillScript : MonoBehaviour {

    public Player player;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 500 * player.dropGauge);
	}
}