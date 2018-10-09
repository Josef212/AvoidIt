using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;
    
    private float movement;

    private GameManager gm;

    // ====================================================================

	void Start ()
    {
        gm = GameManager.Instance;
	}

	void Update ()
    {
        movement = Input.GetAxisRaw("Horizontal");

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            // TODO: Calc the movement for mobile with touches
        }
        
	}

    void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, -movement * movementSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.tag == "Obstacle")
        {
            gm.GameLost();
        }
    }
}
