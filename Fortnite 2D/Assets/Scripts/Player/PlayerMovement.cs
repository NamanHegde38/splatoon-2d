using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof (PlayerController))]
public class PlayerMovement : MonoBehaviour {

	[SerializeField] float jumpHeight = 4;
	[Range (.1f, 1f)] [SerializeField] float timeToJumpApex = .4f;
	[SerializeField] float moveSpeed = 6;

	float accelerationTimeAirborne = .1f;
	float accelerationTimeGrounded = .05f;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	bool facingRight = true;

	PlayerController controller;
	Animator anim;

	void Start () {

		controller = GetComponent<PlayerController>();
		anim = GetComponent<Animator>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update () {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		anim.SetFloat("Speed", Mathf.Abs(velocity.x));

		if (Input.GetButtonDown("Jump") && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		if (!controller.collisions.below) {
			anim.SetBool("Jumping", true);
		}

		else {
			anim.SetBool("Jumping", false);
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		if (velocity.x > 0 && !facingRight) {
			Flip();
		}

		if (velocity.x < 0 && facingRight) {
			Flip();
		}
	}

	void Flip () {

		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}