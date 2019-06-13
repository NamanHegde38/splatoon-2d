using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

	private PlayerMovement _player;
    
    private void Start () {
	    SetComponents();
    }

    private void SetComponents() {
	    _player = GetComponent<PlayerMovement>();
    }

    private void Update () {
	    GetDirectionalInput();
	    GetJumpInput();
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
}
