using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public PlayerController controller;
	public float runSpeed = 40;

	private float horizontalMove = 0f;
	private float jumpCd = 0f;
	private bool jump = false;
	private bool crouch = false;
	private Animator anim;

	void Start () {

		anim = GetComponent<Animator>();
	}

	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

		//Jump Cooldown
		if (jumpCd <= 0) {

			if (Input.GetButtonDown("Jump")) {

				jump = true;
				jumpCd = 0.1f;
				anim.SetBool("Jumping", true);
			}
		}

		else {
			jumpCd -= Time.deltaTime;
		}

		if (Input.GetButtonDown("Crouch")) {
			crouch = true;
		}

		else if (Input.GetButtonUp("Crouch")) {
			crouch = false;
		}
	}

	void FixedUpdate () {

		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	public void OnLanding () {
		anim.SetBool("Jumping", false);
		jump = false;
	}

	public void OnCrouching (bool isCrouching) {
		anim.SetBool("Crouching", isCrouching);
	}
}