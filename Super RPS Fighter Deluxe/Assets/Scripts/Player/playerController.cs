using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    bool rightFacing = false;
    bool isJumping = false;

    float defualtSpeed = 0f;
    int jumps = 1;
    float startingY;
    [SerializeField]
    float fallSpeed;
    [SerializeField]
    float lowjump;
    [SerializeField]
    float jumpVelocity;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float speedInc;
    [SerializeField]
    LayerMask ground;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    int jumpCount;
    [SerializeField]
    Transform groundCheck;

    Rigidbody2D rb;
    BoxCollider2D bc;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        moveSpeed = defualtSpeed;

        jumpCount = 0;
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
        print("Velocity : " + rb.velocity.y);
        print("Vertical : " + vertical);

        if (vertical > 0 && IsGrounded()) {
            rb.velocity = Vector2.up * jumpVelocity;
        }

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeed - 1 ) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && vertical > 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowjump - 1 )* Time.deltaTime;
        }

    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Linecast(position, groundCheck.position, ground);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
