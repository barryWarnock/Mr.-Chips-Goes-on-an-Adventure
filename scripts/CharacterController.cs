using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    float maxSpeed = 8f;
    bool facingRight = true;

    string jump = "Fire1";
    float jumpSpeed = 25f;
    bool jumping = true;

	// Use this for initialization
	void Start () {
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "floor")
            jumping = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        Rigidbody2D physics = GetComponent<Rigidbody2D>();
        float x_vel = (jumping) ? (move * maxSpeed) : (move * maxSpeed);// no control mid-air
        float y_vel = physics.velocity.y;

        if (Input.GetButtonDown(jump) && jumping == false)
        {
            y_vel = jumpSpeed;
            jumping = true;
        }

        physics.velocity = new Vector2(x_vel, y_vel);

        Animator animator = GetComponent<Animator>();
        animator.SetFloat("speed", System.Math.Abs(x_vel));

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
