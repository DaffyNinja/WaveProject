﻿using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(-speed, 0, 0);
	
	}
}
