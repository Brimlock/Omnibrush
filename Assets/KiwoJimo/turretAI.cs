using UnityEngine;
using System.Collections;

public class turretAI : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //_target = other.gameObject.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //_target = null;
        }
    }

}
