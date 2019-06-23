using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class PlayerSquid : MonoBehaviour {
    
    private PlayerController _controller;

	public Material sprite;

	[HideInInspector] public bool _isSquid;

	[SerializeField] private Vector2 _inklingSize = new Vector2(1f, 1f);
	[SerializeField] private Vector2 _squidSize = new Vector2(1f, 0.25f);
    
    private void Start() {
		
    }

    private void SetComponents() {
		_controller = GetComponent<PlayerController>(); 
	}

    private void Update() {
		CheckIfSquid();
    }

	public void SetIsSquid(bool crouchInput) {
		if (crouchInput) {
			_isSquid = true;
		}
		else {
			_isSquid = false;
		}
	}

	private void SquidForm() {
		GetComponent<BoxCollider2D>().size = _squidSize;
		GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.37f);
		sprite.color = Color.blue;
	}

	private void InklingForm() {
		GetComponent<BoxCollider2D>().size = _inklingSize;
		GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
		sprite.color = Color.red;
	}

	private void CheckIfSquid() {
		if (_isSquid) {
			SquidForm();
		}
		else {
			InklingForm();
		}
	}
}
