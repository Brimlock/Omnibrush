using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
    public int range;
    public float speed;
    public bool spin;
    private float spinSpeed;
	// Use this for initialization
	void Start () {
        transform.Rotate(0, 0, Random.Range(-30.0f, 30.0f));
        spinSpeed = Random.Range(1.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y > range)
        {
            transform.position = new Vector3(transform.position.x, -range, transform.position.z);
        }
        else if(transform.position.y < -range)
        {
            transform.position = new Vector3(transform.position.x, range, transform.position.z);
        }
        if(spin) transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        transform.position += new Vector3(0, (speed * Time.deltaTime), 0);
    }
}
