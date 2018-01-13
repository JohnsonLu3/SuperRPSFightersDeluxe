using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    
    // Game Checks
    bool rightFacing = false;
    bool tempFacing = false;
    bool isJumping = false;
    int jumpFrames = 0;
    float defualtSpeed = 0f;
    int jumpDelayFrames = 0;
    bool rockDown = false;
    bool paperDown = false;
    bool scissorDown = false;
    float startingY;

    // Attack Checks
    int rockCombo = 0;
    int paperCombo = 0;
    int scissorCombo = 0;

    int health = 100;


    // Game Qualities

    [SerializeField] string playerNum;
    [SerializeField] float fallSpeed;
    [SerializeField] float lowjump;
    [SerializeField] float jumpVelocity;
    [SerializeField] int jumpDelay;
    [SerializeField] float dashSpeed;
    [SerializeField] LayerMask ground;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform groundCheck;
    [SerializeField] GameObject otherPlayer;
    [SerializeField] BoxCollider2D attackTrigger;
    [SerializeField] GameObject sprintRender;

    // Game Objects
    Rigidbody2D rb;
    BoxCollider2D bc;
    Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = sprintRender.GetComponent<Animator>();
        defualtSpeed = moveSpeed;
        changeFacing();
        tempFacing = rightFacing;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        float horizontal = Input.GetAxis(playerNum + "_Horizontal");
        float vertical = Input.GetAxis(playerNum + "_Vertical");

        if (vertical <= 0) {
            moveSpeed = defualtSpeed;
        }
        changeFacing();

        if (Round.roundStart) {
            movement(horizontal);
            jump(vertical);
            attack();
        }
    }

    void attack()
    {
        float rockAttack = Input.GetAxisRaw(playerNum + "_rock");
        float paperAttack = Input.GetAxisRaw(playerNum + "_paper");
        float scissorAttack = Input.GetAxisRaw(playerNum + "_scissor");

        if (rockAttack == 1 && !rockDown) {
            if (rockCombo < 3)
            {
                rockCombo++;
            }
            paperCombo = 0;
            scissorCombo = 0;

            rockDown = true;

            playComboAnimation("rock", rockCombo);

        }
        else if(paperAttack == 1 && !paperDown)
        {
            if (paperCombo < 3)
            {
                paperCombo++;
            }
            rockCombo = 0;
            scissorCombo = 0;

            paperDown = true;
            playComboAnimation("paper", paperCombo);
        }
        else if(scissorAttack == 1 && !scissorDown)
        {
            if (scissorCombo < 3)
            {
                scissorCombo++;
            }
            rockCombo = 0;
            paperCombo = 0;

            scissorDown = true;

            playComboAnimation("scissor", scissorCombo);
        }
        else
        {
            if (rockAttack == 0 && rockDown)
            {
                rockDown = false;
            }
            else if (paperAttack == 0 && paperDown)
            {
                paperDown = false;
            }
            else if (scissorAttack == 0 && scissorDown)
            {
                scissorDown = false;
            }
        }


    }

    void playComboAnimation(string attackName, int combo) {
        switch (combo)
        {
            case 1:
                anim.SetTrigger(attackName + "1");
                break;
            case 2:
                anim.SetTrigger(attackName + "2");
                break;
            case 3:
                anim.SetTrigger(attackName + "3");
                break;
            default:
                break;
        }
    }

    void movement(float horizontal) {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    void jump(float vertical) {
        if (IsGrounded()) {
            jumpFrames = 0;
            moveSpeed = defualtSpeed;
            if (isJumping) {
                if (jumpDelayFrames == jumpDelay)
                {
                    isJumping = false;
                }
                else {
                    jumpDelayFrames++;
                }
            }
        }

        if (rb.velocity.y > 0 && vertical > 0 && !IsGrounded() && jumpFrames > 0 && jumpFrames < 8)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpVelocity * -1) * Time.deltaTime;


            increaseJumpFrames();
        }
        else if (rb.velocity.y < 0 && !IsGrounded())
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime;
        }
        else if (vertical > 0 && IsGrounded() && rb.velocity.y == 0 && !isJumping)
        {
            rb.velocity = Vector2.up * lowjump;
            increaseJumpFrames();
            isJumping = true;
            jumpDelayFrames = 0;
            anim.SetTrigger("jump");
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

    void changeFacing() {

        setFacing();

        if (tempFacing != rightFacing && rightFacing) 
        {
            //anim.SetTrigger("turn");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            tempFacing = rightFacing;
        }
        else if (tempFacing != rightFacing && !rightFacing) 
        {
            //anim.SetTrigger("turn");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            tempFacing = rightFacing;
        }
    }

    void setFacing() {
        if (otherPlayer.transform.position.x > transform.position.x)
        {
            rightFacing = true;
        }
        else
        {
            rightFacing = false;
        }
    }
}
