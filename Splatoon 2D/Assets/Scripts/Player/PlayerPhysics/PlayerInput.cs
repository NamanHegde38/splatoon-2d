using System.Collections;
using System.Collections.Generic;
using Player.PlayerPhysics;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerSquid))]
public class PlayerInput : MonoBehaviour {

	private PlayerMovement _player;
	private PlayerSquid _squid;
    
    private void Start () {
	    SetComponents();
    }

    private void SetComponents() {
	    _player = GetComponent<PlayerMovement>();
		_squid = GetComponent<PlayerSquid>();
    }

    private void Update () {
	    GetDirectionalInput();
	    GetJumpInput();
		GetCrouchInput();
    }

    private void GetJumpInput() {
	    if (Input.GetButtonDown("Jump")) {
		    _player.OnJumpInputDown();
	    }
    }

    private void GetDirectionalInput() {
	    var directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    _player.SetDirectionalInput(directionalInput);
    }

	private void GetCrouchInput() {
		bool isSquid = false;
		
		if (Input.GetButton("Crouch")) {
			isSquid = true;
		}

		else {
			isSquid = false;
		}

		_squid.SetIsSquid(isSquid);
	}
}
