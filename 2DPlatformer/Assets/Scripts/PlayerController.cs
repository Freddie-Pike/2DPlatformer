/**
 * The script that will control the player.
 * @author: Freddie Pike, Josh Forward.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Rigidbody variable
    private Rigidbody2D rbody2D; 

    // Sprite Renderer Variable
    private SpriteRenderer sRenderer;

    // Animation variables.
    // private Animator anim;

    // Walking variables
    public float moveSpeed;
    private float moveVelocity;

    // Jumping variables.
    public float jumpShortSpeed = 3f; // Minimum jumping distance.
    public float jumpSpeed = 6f; // Maximum jumping distance.
    bool jump = false;
    bool jumpCancel = false;

    // Ground Checking variables.
    public Transform groundCheck;
    public float groundCheckRadious;
    public LayerMask whatIsGround;
    public bool grounded;


	// Use this for initialization
	void Start () {
        // Grab components on the player.
        rbody2D = GetComponent<Rigidbody2D>(); // Rigidbody 2D.
        sRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer.
        // anim = GetComponent<Animator> (); // Animator.
	}

    // Used to determine if jumping has started or ended.
    void FixedUpdate()
    {
        // Let the player jump into the air..
        if (jump) 
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
            jump = false;
        }

        // If the player lets go of the jump button, the player will fall.
        if (jumpCancel) {
            // If velocity.y is less than the jumpShortSpeed variable, fall to the ground.
            if (GetComponent<Rigidbody2D>().velocity.y > jumpShortSpeed) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpShortSpeed);
            }
            jumpCancel = false; 
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Boolean that will check whether mega man is on the ground or not. 
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadious, whatIsGround);
        // anim.SetBool ("Grounded", grounded); // For Jumping animations.

        // Determine how fast the player will move.
        moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");
        rbody2D.velocity = new Vector2(moveVelocity, rbody2D.velocity.y);
        // anim.SetFloat ("Speed", Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x)); // For Walking animations.

        // If-else statement to determine which direction the player is facing.
        if (GetComponent<Rigidbody2D> ().velocity.x > 0) 
        {
            sRenderer.flipX = false;
        } 
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            sRenderer.flipX = true;
        }

        // If jump key is pressed down then let player jump if player is on the ground.
        if (Input.GetButtonDown ("Jump") && grounded) {
            jump = true;
        }

        // If jump key is let go and the player isn't grounded then the jump is over.
        if (Input.GetButtonUp ("Jump") && !grounded) {
            jumpCancel = true;
        }
	}
}