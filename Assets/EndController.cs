﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene(0);
    }
}
