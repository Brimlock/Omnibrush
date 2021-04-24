using UnityEngine;
using System.Collections;

public class pickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 45f * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            if (other.gameObject.GetComponent<Player>().dropGauge < 1.0f) other.gameObject.GetComponent<Player>().dropGauge += 0.1f;
            GameObject.Destroy(gameObject);

        }
    }
}
