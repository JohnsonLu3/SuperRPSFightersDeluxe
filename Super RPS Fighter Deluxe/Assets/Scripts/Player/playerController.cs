using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    bool rightFacing = false;
    float defualtSpeed = 0f;

    [SerializeField]
    float fallSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float speedInc;
    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = defualtSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("p1_Horizontal");
        movementHandler(horizontal);

    }

    void movementHandler(float horizontal) {
        if (horizontal == 0) {
            moveSpeed = defualtSpeed;
        }
        if (moveSpeed < maxSpeed) {
            moveSpeed += speedInc;
        }
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }
}
