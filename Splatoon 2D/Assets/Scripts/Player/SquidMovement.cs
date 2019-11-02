using System;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerController))]
    public class SquidMovement : MonoBehaviour {
        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float timeToJumpApex = .4f;
        private const float AccelerationTimeAirborne = .5f;
        private const float AccelerationTimeGrounded = .3f;
        private const float MoveSpeed = 10;

        [HideInInspector] public float gravity;
        private float _maxJumpVelocity;
        private float _minJumpVelocity;
        [HideInInspector] public Vector3 velocity;
        private float _velocityXSmoothing;

        private PlayerController _controller;

        private Vector2 _directionalInput;

        private void Start() {
            _controller = GetComponent<PlayerController>();

            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            _maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            _minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        }

        private void Update() {
            CalculateVelocity();
            _controller.Move(velocity * Time.deltaTime, _directionalInput);

            if (_controller.collisions.Above || _controller.collisions.Below) {
                if (_controller.collisions.SlidingDownMaxSlope) {
                    velocity.y += _controller.collisions.SlopeNormal.y * -gravity * Time.deltaTime;
                }
                else {
                    velocity.y = 0;
                }
            }
        }

        public void SetDirectionalInput(Vector2 input) {
            _directionalInput = input;
        }

        public void OnJumpInputDown() {

            if (_controller.collisions.Below) {
                if (_controller.collisions.SlidingDownMaxSlope) {
                    if (Math.Abs(_directionalInput.x - (-Mathf.Sign(_controller.collisions.SlopeNormal.x))) > Mathf.Epsilon) {
                        // not jumping against max slope
                        velocity.y = _maxJumpVelocity * _controller.collisions.SlopeNormal.y;
                        velocity.x = _maxJumpVelocity * _controller.collisions.SlopeNormal.x;
                    }
                }
                else {
                    velocity.y = _maxJumpVelocity;
                }
            }
        }

        public void OnJumpInputUp() {
            if (velocity.y > _minJumpVelocity) {
                velocity.y = _minJumpVelocity;
            }
        }

        private void CalculateVelocity() {
            var targetVelocityX = _directionalInput.x * MoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref _velocityXSmoothing,
                (_controller.collisions.Below) ? AccelerationTimeGrounded : AccelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
        }
    }
}