using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    bool rightFacing = false;
    bool falling = false;
    int jumpFrames = 0; 

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
    float speedMul;
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
        defualtSpeed = moveSpeed;

        jumpCount = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        float horizontal = Input.GetAxis("p1_Horizontal");
        float vertical = Input.GetAxis("p1_Vertical");

        if (horizontal == 0 || vertical <= 0) {
            moveSpeed = defualtSpeed;
        }

        movement(horizontal);
        jump(vertical);
    }

    void movement(float horizontal) {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    void jump(float vertical) {
        if (IsGrounded()) {
            jumpFrames = 0;
            moveSpeed = defualtSpeed;
        }

        if (rb.velocity.y > 0 && vertical > 0 && !IsGrounded() && jumpFrames > 0 && jumpFrames < 10)
        {
            print("Jumping -- frame : " + jumpFrames + " Vertical : " + vertical + " velocity : " + rb.velocity.y);
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpVelocity * -1) * Time.deltaTime;


            increaseJumpFrames();
        }
        else if (rb.velocity.y < 0 && !IsGrounded())
        {
            print("fall -- frame : " + jumpFrames + " Vertical : " + vertical + " velocity : " + rb.velocity.y);
            falling = true;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime;
            if (jumpFrames > 0 && jumpFrames < 7)
            {
                moveSpeed = maxSpeed;
            }


        }
        else if (vertical > 0 && IsGrounded() && rb.velocity.y == 0 && !falling)
        {
            print("startjump -- frame : " + jumpFrames + " Vertical : " + vertical + " velocity : " + rb.velocity.y);
            rb.velocity = Vector2.up * lowjump;
            increaseJumpFrames();
        }
        else {
            falling = false;
        }

    }

    void increaseJumpFrames() {
        if (jumpFrames < int.MaxValue) {
            jumpFrames++;
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
