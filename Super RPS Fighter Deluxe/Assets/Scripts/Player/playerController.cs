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
    LayerMask ground;
    [SerializeField]
    float moveSpeed;

    float groundDistance;
    Rigidbody2D rb;
    BoxCollider2D bc;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        moveSpeed = defualtSpeed;
        groundDistance = bc.bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("p1_Horizontal");
        float vertical = Input.GetAxis("p1_Vertical");
        movement(horizontal);
        jump(vertical);
    }

    void movement(float horizontal) {
        if (horizontal == 0) {
            moveSpeed = defualtSpeed;
        }
        if (moveSpeed < maxSpeed) {
            moveSpeed += speedInc;
        }
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    void jump(float vertical) {
        if (vertical > 0 && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, vertical * jumpForce);
        }
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, ground);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
