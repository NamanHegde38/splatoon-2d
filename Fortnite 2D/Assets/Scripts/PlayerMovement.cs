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
	private Rigidbody2D rigidBody;


	void Awake () {

		rigidBody = GetComponent<Rigidbody2D>();
		currentSpeed = walkSpeed;
	}
	
	void Update () {
		JumpRequest();
	}

	void FixedUpdate () {

		Walk();
		Jump();
		Crouch();
	}

	void Walk () {

		if (Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector2.left * currentSpeed * Time.fixedDeltaTime);
			transform.localScale = new Vector3(-6, 6, 1);
		}

		else if (Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector2.right * currentSpeed * Time.fixedDeltaTime);
			transform.localScale = new Vector3(6, 6, 1);
		}
	}

	void Jump() {

		if (jumpRequest) {
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			jumpRequest = false;
		}

		if (rigidBody.velocity.y < 0) {
			rigidBody.gravityScale = fallMultiplier;
		}

		else {
			rigidBody.gravityScale = 1;
		}
	}

	void JumpRequest() {

		if (Input.GetKeyDown(jump)) {
			jumpRequest = true;
		}
	}

	void Crouch () {

		if (Input.GetKeyDown(crouch) && !crouchEnabled) {
			currentSpeed = crouchSpeed;
			crouchEnabled = true;
		}

		else if (Input.GetKeyDown(crouch) && crouchEnabled) {
			currentSpeed = walkSpeed;
			crouchEnabled = false;
		}
	}	
}
