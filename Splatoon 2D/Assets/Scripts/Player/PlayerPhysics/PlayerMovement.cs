using Player.PlayerPhysics;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 6f;
	public float slowedMoveSpeed = 3f;

	public float squidMoveSpeed = 2f;
	public float squidInkSpeed = 10f;
	public float squidSlowedSpeed = 1f;

	private float _currentSpeed;
	
	public float jumpHeight = 4;
	[Range (.1f, 1f)] public float timeToJumpApex = .4f;

	private const float _accelerationTimeAir = .1f;
	private const float _accelerationTimeGround = .05f;

	private float _gravity;

	private float _jumpVelocity;

	private float _jumpCooldown = 0.1f;
	private bool _ableToJump;

	private bool _hasSetCooldown;

	private Vector3 _velocity;
	private float _velocityXSmoothing;
	private Vector2 _directionalInput;

	private string _groundInk;

	public bool facingRight = true;

	private bool _isSquid = false;

	private PlayerController _controller;
	private PlayerSquid _squid;
	private Animator _anim;

	private static readonly int Jumping = Animator.StringToHash("Jumping");
	private static readonly int Speed = Animator.StringToHash("Speed");

	private void Start () {
		SetComponents();
		SetPhysics();
	}

	private void SetPhysics() {
		_gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		_jumpVelocity = Mathf.Abs(_gravity) * timeToJumpApex;
	}

	private void SetComponents() {
		_controller = GetComponent<PlayerController>();
		_squid = GetComponent<PlayerSquid>();
		_anim = GetComponent<Animator>();
	}

	public void SetDirectionalInput (Vector2 input) {
		_directionalInput = input;
	}

	public void OnJumpInputDown () {
		if (_ableToJump) {
			if (_isSquid) {
				_velocity.y = _jumpVelocity / 1.5f;
			}
			else {
				_velocity.y = _jumpVelocity;
			}
			_ableToJump = false;
		}
	}

	private void Update () {
		SetAnimationParameters();
		SetVelocity();
		SetJumpDelay();
		CheckGroundInk();
		SetMovementSpeed();
		CheckIfSquid();
	}

	private void SetVelocity() {
		var targetVelocityX = _directionalInput.x * _currentSpeed;

		_velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocityX, ref _velocityXSmoothing,
			(_controller.collisions.below) ? _accelerationTimeGround : _accelerationTimeAir);

		_velocity.y += _gravity * Time.deltaTime;

		_controller.Move(_velocity * Time.deltaTime);

		if (_controller.collisions.above || _controller.collisions.below) {
			_velocity.y = 0;
		}
	}

	private void SetMovementSpeed() {
		if (_isSquid) {
			if (_groundInk == "Unpainted ground") {
			_currentSpeed = squidMoveSpeed;
			}

			else if (_groundInk == "Team painted ground") {
			_currentSpeed = squidInkSpeed;
			}
			
			else if (_groundInk == "Enemy painted ground") {
			_currentSpeed = squidSlowedSpeed;
			}
		}

		else {
			if (_groundInk == "Unpainted ground" || _groundInk == "Team painted ground") {
			_currentSpeed = moveSpeed;
			}
			
			else if (_groundInk == "Enemy painted ground") {
			_currentSpeed = slowedMoveSpeed;
			}
		}
	}

	private void CheckGroundInk() {
		if (_controller.collisions.below) {
			_groundInk = _controller.CheckGroundTag();
		}
	}

	private void SetJumpDelay() {
		if (!_controller.collisions.below) {
			if (!_hasSetCooldown) {
				_jumpCooldown = 0.1f;
				_hasSetCooldown = true;
			}
		}

		else {
			_hasSetCooldown = false;
			_ableToJump = true;
		}

		if (_jumpCooldown <= 0 && !_controller.collisions.below) {
			_ableToJump = false;
		}

		else {
			_jumpCooldown -= Time.deltaTime;
			_ableToJump = true;
		}
	}

	private void CheckIfSquid() {
		_isSquid = _squid.receivingCrouchInput;
	}

	private void SetAnimationParameters() {
		_anim.SetFloat(Speed, Mathf.Abs(_velocity.x));

		if (!_controller.collisions.below) {
			_anim.SetBool(Jumping, true);
		}

		else {
			_anim.SetBool(Jumping, false);
		}
	}

	public void Flip () {
		facingRight = !facingRight;

		var transformVariable = transform;
		var scale = transformVariable.localScale;
		scale.x *= -1;

		transformVariable.localScale = scale;
	}
}