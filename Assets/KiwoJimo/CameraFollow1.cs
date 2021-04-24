using UnityEngine;
using System.Collections;

public class CameraFollow1 : MonoBehaviour {
    public Transform target;
    public int radius;
    private Vector3 mouseOrigin;
    public float sensitivity;
    public float vertical;
    //private Vector3 _movement;
    //public int speed = 5;

    // Use this for initialization
    void Start () {
        mouseOrigin = Input.mousePosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 delta = Input.mousePosition - mouseOrigin;
        vertical = Mathf.Clamp(vertical + delta.y * sensitivity, Mathf.PI / 9, (4 * Mathf.PI)/ 9);
        //vertical += delta.y * sensitivity;
        transform.localPosition = new Vector3(0, Mathf.Sin(vertical) * radius / target.lossyScale.y, - radius * Mathf.Cos(vertical)/ target.lossyScale.z);
         mouseOrigin = Input.mousePosition;
         transform.LookAt(target);
        //Debug.Log(Mathf.Pow(transform.localPosition.y,2) + Mathf.Pow(transform.localPosition.z, 2));

    }
}
