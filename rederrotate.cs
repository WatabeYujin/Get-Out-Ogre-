﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rederrotate : MonoBehaviour {
    [SerializeField]
    private Transform maincamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(90, 0, -maincamera.eulerAngles.y);
	}
}
