using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject previewPaint;
    public GameObject paint;
    public int speed = 5;
    public float dropGauge;
    public Vector3 respawn;
    private int paintPool = 1;
    private Renderer _renderer;

    private Vector3 _movement;
    private CharacterController _controller;

    private Vector3 mouseOrigin;
    public float sensitivity;
    public float jumpVelocity;
    // Use this for initialization


    private GameObject vozdukh0;
    void Start ()
    {
        _controller = GetComponent<CharacterController>();
        _renderer = GetComponent<Renderer>();
        float a = _renderer.sharedMaterial.color.a;
        float r = _renderer.sharedMaterial.color.r;
        float g = _renderer.sharedMaterial.color.g;
        float b = _renderer.sharedMaterial.color.b;
        _renderer.sharedMaterial.color = new Color(r, g, b, dropGauge);
        mouseOrigin = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < -50) reset();
        if (Input.GetKey(KeyCode.Q)) { transform.Rotate(new Vector3(0, -90 * Time.deltaTime, 0)); }
        if (Input.GetKey(KeyCode.E)){transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));}
        _movement.x = Input.GetAxis("Horizontal") * speed;
        _movement.z = Input.GetAxis("Vertical") * speed;
        if(_controller.isGrounded)
        {
            _movement.y = 0;
            if (Input.GetKeyDown(KeyCode.Space)){
                _movement.y = jumpVelocity;
            }
        }
        _movement.y += Physics.gravity.y;
        _movement = transform.TransformDirection(_movement);
        _controller.Move(_movement * Time.deltaTime);
        if(checkPaintForward())
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (dropGauge >= 0.09f)
                {
                    dropGauge -= 0.1f;
                    GameObject vozdukh = Instantiate(paint, new Vector3(((Mathf.Round(transform.position.x / 20) + getAngleAdjustment(true)) * 20), 0, (((Mathf.Round(transform.position.z / 20)) + getAngleAdjustment(false)) * 20)), new Quaternion()) as GameObject;
                    vozdukh.name = "Vozdukh";
                }
            }
        }
        /*if (checkPaintBackward())
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (dropGauge >= 0.09f)
                {
                    dropGauge -= 0.1f;
                    GameObject vozdukh = Instantiate(paint, new Vector3(((Mathf.Round(transform.position.x / 20)) * 20), 0, (((Mathf.Round(transform.position.z / 20)) - 1) * 20)), new Quaternion()) as GameObject;
                    vozdukh.name = "Vozdukh";
                }
            }
        }
        if (checkPaintRightward())
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (dropGauge >= 0.09f)
                {
                    dropGauge -= 0.1f;
                    GameObject vozdukh = Instantiate(paint, new Vector3((Mathf.Round(transform.position.x / 20) + 1) * 20, 0, Mathf.Round(transform.position.z / 20) * 20), new Quaternion()) as GameObject;
                    vozdukh.name = "Vozdukh";
                }
            }
        }
        if (checkPaintLeftward())
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (dropGauge >= 0.09f)
                {
                    dropGauge -= 0.1f;
                    GameObject vozdukh = Instantiate(paint, new Vector3((Mathf.Round(transform.position.x / 20) - 1) * 20, 0, Mathf.Round(transform.position.z / 20) * 20), new Quaternion()) as GameObject;
                    vozdukh.name = "Vozdukh";
                }
            }
        }*/
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "VozdukhHalf")
        {
            Component.Destroy(hit.collider.GetComponent<Collider>());
        }
        if (hit.collider.gameObject.tag == "Slime")
        {
            if (dropGauge >= 0.09f) dropGauge -= 0.1f;
            else reset();
            //StartCoroutine("blink");
        }
        if (hit.collider.gameObject.tag == "Win")
        {
            /*float a = _renderer.sharedMaterial.color.a;
            float r = _renderer.sharedMaterial.color.r;
            float g = _renderer.sharedMaterial.color.g;
            float b = _renderer.sharedMaterial.color.b;
            Debug.LogFormat("r={0}, g={1}, b={2}, a={3}", r, g, b, a);

            //if (a > 0.0f) _renderer.sharedMaterial.color = new Color(r, g, b, a - 0.01f);
            //reset();*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    float getAngleAdjustment(bool x)
    {
        if(x)
            return Mathf.Abs(transform.forward.x) > Mathf.Abs(transform.forward.z) ? Mathf.Sign(transform.forward.x) : 0;
        else
            return Mathf.Abs(transform.forward.z) > Mathf.Abs(transform.forward.x) ? Mathf.Sign(transform.forward.z) : 0;
    }

    bool checkPaintForward()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10 + new Vector3(0, -5, 0));
        //Ray frontRay = new Ray(transform.position, new Vector3(0, -5, 20));
        Ray frontRay = new Ray(transform.position, transform.forward * 10 + new Vector3(0, -5, 0));
        if(!Physics.Raycast(frontRay, out hit)) //hit nought
        {
            if (GameObject.Find("VozdukhHalf0") == null)
            {
                vozdukh0 = Instantiate(previewPaint, new Vector3(((Mathf.Round(transform.position.x / 20) + getAngleAdjustment(true)) * 20), 0, (((Mathf.Round(transform.position.z / 20)) + getAngleAdjustment(false)) * 20)), new Quaternion()) as GameObject;
                Material mat = vozdukh0.GetComponent<Renderer>().material;
                vozdukh0.name = "VozdukhHalf0";
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a / 2);
            }
            else
            {
                GameObject.Find("VozdukhHalf0").transform.position = new Vector3(((Mathf.Round(transform.position.x / 20) + getAngleAdjustment(true)) * 20), 0, (((Mathf.Round(transform.position.z / 20)) + getAngleAdjustment(false)) * 20));
                Debug.Log("Moving: " + new Vector3(((Mathf.Round(transform.position.x / 20) + getAngleAdjustment(true)) * 20), 0, (((Mathf.Round(transform.position.z / 20)) + getAngleAdjustment(false)) * 20)));
            }
            return true;
        }
        else if(hit.collider.gameObject != GameObject.Find("VozdukhHalf0")) //hit something else
        {
            if (GameObject.Find("VozdukhHalf0") != null) { GameObject.Destroy(GameObject.Find("VozdukhHalf0")); }
            return false;
        }
        return true; // hit clone
    }
    bool checkPaintBackward()
    {
        RaycastHit hit;
        Ray backRay = new Ray(transform.position, new Vector3(0, -5, -20));
        //Ray backRay = new Ray(transform.position, transform.forward * -10 + new Vector3(0, -5, 0));
        Debug.DrawRay(transform.position, transform.forward * 10 + new Vector3(0, -5, 0));
        GameObject vozdukh1;
        if (!Physics.Raycast(backRay, out hit)) //hit nought
        {
            if (GameObject.Find("VozdukhHalf1") == null)
            {
                vozdukh1 = Instantiate(previewPaint, new Vector3(((Mathf.Round(transform.position.x / 20)) * 20), 0, (((Mathf.Round(transform.position.z / 20)) - 1) * 20)), new Quaternion()) as GameObject;
                vozdukh1.name = "VozdukhHalf1";
                Material mat = vozdukh1.GetComponent<Renderer>().material;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a / 2);
            }
            return true;
        }
        else if (hit.collider.gameObject != GameObject.Find("VozdukhHalf1")) //hit something else
        {
            if (GameObject.Find("VozdukhHalf1") != null) { GameObject.Destroy(GameObject.Find("VozdukhHalf1")); }
            return false;
        }
        return true; // hit clone
    }
    bool checkPaintLeftward()
    {
        RaycastHit hit;
        Ray leftRay = new Ray(transform.position, new Vector3(-20, -5, 0));
        GameObject vozdukh3;
        if (!Physics.Raycast(leftRay, out hit)) //hit nought
        {
            if (GameObject.Find("VozdukhHalf3") == null)
            {
                vozdukh3 = Instantiate(previewPaint, new Vector3((Mathf.Round(transform.position.x / 20) - 1) * 20, 0, Mathf.Round(transform.position.z / 20) * 20), new Quaternion()) as GameObject;
                vozdukh3.name = "VozdukhHalf3";
                Material mat = vozdukh3.GetComponent<Renderer>().material;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a / 2);
            }
            return true;
        }
        else if (hit.collider.gameObject != GameObject.Find("VozdukhHalf3")) //hit something else
        {
            if (GameObject.Find("VozdukhHalf3") != null) { GameObject.Destroy(GameObject.Find("VozdukhHalf3")); }
            return false;
        }
        return true; // hit clone

    }
    bool checkPaintRightward()
    {
        RaycastHit hit;
        Ray rightRay = new Ray(transform.position, new Vector3(20, -5, 0));
        GameObject vozdukh2;
        if (!Physics.Raycast(rightRay, out hit)) //hit nought
        {
            if (GameObject.Find("VozdukhHalf2") == null)
            {
                vozdukh2 = Instantiate(previewPaint, new Vector3((Mathf.Round(transform.position.x/20)+1) * 20, 0, Mathf.Round(transform.position.z/20) * 20), new Quaternion()) as GameObject;
                vozdukh2.name = "VozdukhHalf2";
                Material mat = vozdukh2.GetComponent<Renderer>().material;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a /2);
            }
            return true;
        }
        else if (hit.collider.gameObject != GameObject.Find("VozdukhHalf2")) //hit something else
        {
            if (GameObject.Find("VozdukhHalf2") != null) { GameObject.Destroy(GameObject.Find("VozdukhHalf2")); }
            return false;
        }
        return true; // hit clone
    }
    /*IEnumerator blink()
    {
        Material[] mats = _renderer.materials;
        while (_renderer.material.color.a > 0.0f)
        {
            for (int i = 0; i < mats.Length; i++)
            {
                _renderer.materials.
                //renderer.materials.set().color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 0.01f);
                yield return null;
            }
        }
    }*/

    public void reset()
    {
        transform.position = respawn;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Drop") paintPool++;
    }

    void FixedUpdate()
    {
        Vector3 delta = Input.mousePosition - mouseOrigin;
        mouseOrigin = Input.mousePosition;
        transform.Rotate(0, delta.x * sensitivity, 0);
    }
}