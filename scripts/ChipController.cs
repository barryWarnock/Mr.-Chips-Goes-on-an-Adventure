using UnityEngine;
using System.Collections;

public class 
    ChipController : MonoBehaviour {

    float maxSpeed = 8f;
    bool facingRight = true;

    string jump = "Fire1";
    float jumpSpeed = 19f;
    bool jumping = true;

    Rigidbody2D physics;
    Animator animator;

    // Use this for initialization
    void Start () {
        physics = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "floor" && physics.velocity.y <= 0)
            jumping = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        float x_vel = (jumping) ? (move * maxSpeed) : (move * maxSpeed);// no control mid-air
        float y_vel = physics.velocity.y;

        //determine if falling
        if (y_vel < 0)
            jumping = true;

        if (Input.GetButtonDown(jump) && !jumping)
        {
            y_vel = jumpSpeed;
            jumping = true;
        }

        physics.velocity = new Vector2(x_vel, y_vel);

        
        animator.SetFloat("speed", System.Math.Abs(x_vel));
        animator.SetBool("jumping", jumping);

        if (move > 0 && !facingRight)
            Flip();
        if (move < 0 && facingRight)
            Flip();
	}

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
