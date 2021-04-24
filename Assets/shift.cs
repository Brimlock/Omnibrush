using UnityEngine;
using System.Collections;

public class shift : MonoBehaviour {
    public float speed;
    public Vector3 shiftPos;
    private Vector3 startPos;
    private bool reached;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!reached)
        {
            transform.position = Vector3.Lerp(transform.position, shiftPos, speed * Time.deltaTime/Vector3.Distance(transform.position, shiftPos));
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, speed * Time.deltaTime/Vector3.Distance(transform.position, startPos));
        }
        if (Vector3.Distance(transform.position, startPos) < 0.1f) reached = false;
        else if (Vector3.Distance(transform.position, shiftPos) < 0.1f) reached = true;
	}
}
