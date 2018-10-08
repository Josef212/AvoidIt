using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 0.5f;
    public GameObject obstaclePrefab;

    private float nextSpawn;

	void Start ()
    {
        nextSpawn = 1f / spawnRate;
	}
	

	void Update ()
    {
		if(Time.time >= nextSpawn)
        {
            Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity);
            nextSpawn = Time.time + (1f / spawnRate);
        }
	}
}
