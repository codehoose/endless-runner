﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundY : MonoBehaviour {

	public float degrees = 90f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up, degrees * Time.deltaTime, Space.Self);
	}
}
