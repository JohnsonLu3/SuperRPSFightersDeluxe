using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    bool rightFacing = false;
    float fallSpeed = 5f;
    float jumpForce = 5f;
    public float moveSpeed = 2f;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(Controls.PLAYER1_LEFT)) {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(Controls.PLAYER1_RIGHT))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        } else if (Input.GetKey(Controls.PLAYER1_ROCK)) {

        } else if (Input.GetKey(Controls.PLAYER1_PAPER)) {

        }
        else if (Input.GetKey(Controls.PLAYER1_SCISSORS))
        {

        }
    }
}
