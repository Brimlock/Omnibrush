using UnityEngine;
using System.Collections;

public class slimeAI : MonoBehaviour {

    public int speed;
    private bool _ashrinking;
    private bool[] _dir; //0 == right (true) / left, 1 == up (true) / down
    private float[] _wanderLimit;
    private CharacterController _controller;
    private GameObject target;

    private Transform _target;

    private Vector3 _movement, _startPos, _startScale;

	// Use this for initialization
	void Start () {
        _ashrinking = false;
        _dir = new bool[]{false, false};
        _wanderLimit = new float[]{5.0f, 5.0f};
        _controller = GetComponent<CharacterController>();
        _startPos = transform.position;
        _startScale = transform.localScale;

    }
	
	// Update is called once per frame
	void Update ()
    {
        _shrink();
        if (target == null)
        {
            _wander();
        }else
        {
            chase();
        }
        _gravity();
        _controller.Move(_movement);
    }

    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject == target)
        {
            target.GetComponent<Player>().reset();
        }
    }*/
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.GetComponent<Player>() != null)
        {
            target = c.gameObject;
        }
    }

    void chase()
    {
        if(transform.position.x < speed/2 + target.transform.position.x && transform.position.x > -speed/2 + target.transform.position.x) _movement.x = 0;
        else _movement.x = transform.position.x > target.transform.position.x ? -speed * 2 : speed * 2;
        if (transform.position.z < speed/2 + target.transform.position.z && transform.position.z > -speed/2 + target.transform.position.z) _movement.z = 0;
        else _movement.z = transform.position.z > target.transform.position.z ? -speed * 2 : speed * 2;
        _movement.x *= Time.deltaTime;
        _movement.z *= Time.deltaTime;
    }

    void OnTriggerExit(Collider c)
    {
        if(c.gameObject == target)
        {
            target = null;
        }
    }

    void _gravity()
    {
        if (!_controller.isGrounded)
        {
            _movement.y += (Physics.gravity.y * Time.deltaTime);
        }else
        {
            _movement.y = 0;
        }
    }

    private void _shrink()
    {
        if (transform.localScale.x < _startScale.x - 0.5f) _ashrinking = false;
        else if (transform.localScale.x >= _startScale.x + 0.5f) _ashrinking = true;

        transform.localScale += new Vector3((_ashrinking ? -0.01f : 0.01f), (_ashrinking ? -0.01f : 0.01f), (_ashrinking ? -0.01f : 0.01f));
    }

    private void _wander()
    {
        if (transform.position.x - _startPos.x > _wanderLimit[0]) //right-bound
        {
            _dir[0] = false; _wanderLimit[0] = Random.Range(3.0f, 7.0f);
        }
        else if (transform.position.x - _startPos.x < -_wanderLimit[0]) //left-bound
        {
            _dir[0] = true; _wanderLimit[0] = Random.Range(3.0f, 7.0f);
        }

        if (transform.position.z - _startPos.z > _wanderLimit[1])
        {
            _dir[1] = false; _wanderLimit[1] = Random.Range(3.0f, 7.0f);
        }
        else if (transform.position.z - _startPos.z < -_wanderLimit[1])
        {
            _dir[1] = true; _wanderLimit[1] = Random.Range(3.0f, 7.0f);
        }

        _movement.x = ((_dir[0]) ? 1 : -1) * speed * Time.deltaTime;
        _movement.z = ((_dir[1]) ? 1 : -1) * speed * Time.deltaTime;
    }
}
