using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;
    
    private float movement;

    // ====================================================================

	void Start ()
    {

	}

	void Update ()
    {
        movement = Input.GetAxisRaw("Horizontal");
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
            // TODO: Game over
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
