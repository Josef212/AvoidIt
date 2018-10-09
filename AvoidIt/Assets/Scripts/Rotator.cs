﻿using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 30f;

	
	void Update ()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
	}
}
