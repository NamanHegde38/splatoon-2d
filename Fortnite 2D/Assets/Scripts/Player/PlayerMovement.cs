using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float walkSpeed = 4.5f;
	public float jumpSpeed = 5;
	public float crouchSpeed = 2;
	public KeyCode jump;
	public KeyCode crouch;

	private float currentSpeed;
	private float fallMultiplier = 2.5f;
	private bool crouchEnabled;
	private bool jumpRequest;
	private bool isGrounded;
	private Vector2 playerSize;
	private Vector2 boxSize;
	private Vector3 scale;
	private Rigidbody2D rigidBody;
	private Animator anim;


	void Awake () {
		scale = transform.localScale;
		rigidBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		currentSpeed = walkSpeed;
	}
	
	void Update () {
		Walk();
		JumpRequest();
		Crouch();
	}

	void FixedUpdate () {
		Jump();
	}

	void OnCollisionEnter2D (Collision2D collision) {

		if (collision.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void Walk () {

		if (Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector2.left * currentSpeed * Time.fixedDeltaTime);
			transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
			anim.SetBool("Walk", true);
		}

		if (Input.GetKeyUp(KeyCode.A)) {
			anim.SetBool("Walk", false);
		}

		else if (Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector2.right * currentSpeed * Time.fixedDeltaTime);
			transform.localScale =  scale;
			anim.SetBool("Walk", true);
		}

		if (Input.GetKeyUp(KeyCode.D)) {
			anim.SetBool("Walk", false);
		}
	}

	void Jump() {

		if (jumpRequest) {
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			anim.SetBool("Jump", true);
			jumpRequest = false;
			isGrounded = false;
		}

		if (rigidBody.velocity.y < 0) {
			rigidBody.gravityScale = fallMultiplier;
		}

		else {
			rigidBody.gravityScale = 1;
		}

		if (rigidBody.velocity.y == 0) {
			anim.SetBool("Jump", false);
		}
	}

	void JumpRequest() {

		if (Input.GetKeyDown(jump) && isGrounded) {
			jumpRequest = true;
		}
	}

	void Crouch () {

		if (Input.GetKeyDown(crouch) && isGrounded && !crouchEnabled) {
			currentSpeed = crouchSpeed;
			crouchEnabled = true;
		}

		else if (Input.GetKeyDown(crouch) && isGrounded && crouchEnabled) {
			currentSpeed = walkSpeed;
			crouchEnabled = false;
		}

		anim.SetBool("Crouch", crouchEnabled);
	}
}
