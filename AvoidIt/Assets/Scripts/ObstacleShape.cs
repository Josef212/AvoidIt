using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShape : MonoBehaviour
{
    public float shrinkSpeed = 5f;
    public float initialSize = 10f;

    public int points = 1;

    public Rigidbody2D rb;

    private GameManager gm;

	void Start ()
    {
        gm = GameManager.Instance;
        transform.localScale = Vector3.one * initialSize;
        rb.rotation = Random.Range(0f, 360f);
	}

	void Update ()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        if(transform.localScale.x <= 0.5)
        {
            gm.AddPoints(points);
            Destroy(gameObject);
        }
	}
}
