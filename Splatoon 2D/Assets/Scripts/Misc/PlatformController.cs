using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Misc {
	public class PlatformController : PlayerRaycast {

		public LayerMask passengerMask;

		public Vector3[] localWaypoints;
		private Vector3[] _globalWaypoints;

		public float speed;
		public bool cyclic;
		public float waitTime;
		[Range(0,2)]
		public float easeAmount;

		private int _fromWaypointIndex;
		private float _percentBetweenWaypoints;
		private float _nextMoveTime;

		private List<PassengerMovement> _passengerMovement;
		private readonly Dictionary<Transform,PlayerController> _passengerDictionary = new Dictionary<Transform, PlayerController>();
	
		public override void Start () {
			base.Start ();

			_globalWaypoints = new Vector3[localWaypoints.Length];
			for (var i =0; i < localWaypoints.Length; i++) _globalWaypoints[i] = localWaypoints[i] + transform.position;
		}

		private void Update () {

			UpdateRaycastOrigins ();

			var velocity = CalculatePlatformMovement();

			CalculatePassengerMovement(velocity);

			MovePassengers (true);
			transform.Translate (velocity);
			MovePassengers (false);
		}

		private float Ease(float x) {
			var a = easeAmount + 1;
			return Mathf.Pow(x,a) / (Mathf.Pow(x,a) + Mathf.Pow(1-x,a));
		}

		private Vector3 CalculatePlatformMovement() {

			if (Time.time < _nextMoveTime) return Vector3.zero;

			_fromWaypointIndex %= _globalWaypoints.Length;
			var toWaypointIndex = (_fromWaypointIndex + 1) % _globalWaypoints.Length;
			var distanceBetweenWaypoints = Vector3.Distance (_globalWaypoints [_fromWaypointIndex], _globalWaypoints [toWaypointIndex]);
			_percentBetweenWaypoints += Time.deltaTime * speed/distanceBetweenWaypoints;
			_percentBetweenWaypoints = Mathf.Clamp01 (_percentBetweenWaypoints);
			var easedPercentBetweenWaypoints = Ease (_percentBetweenWaypoints);

			var newPos = Vector3.Lerp (_globalWaypoints [_fromWaypointIndex], _globalWaypoints [toWaypointIndex], easedPercentBetweenWaypoints);

			if (_percentBetweenWaypoints >= 1) {
				_percentBetweenWaypoints = 0;
				_fromWaypointIndex ++;

				if (!cyclic)
					if (_fromWaypointIndex >= _globalWaypoints.Length-1) {
						_fromWaypointIndex = 0;
						Array.Reverse(_globalWaypoints);
					}

				_nextMoveTime = Time.time + waitTime;
			}

			return newPos - transform.position;
		}

		private void MovePassengers(bool beforeMovePlatform) {
			foreach (var passenger in _passengerMovement) {
				if (!_passengerDictionary.ContainsKey(passenger.Transform)) _passengerDictionary.Add(passenger.Transform,passenger.Transform.GetComponent<PlayerController>());

				if (passenger.MoveBeforePlatform == beforeMovePlatform) _passengerDictionary[passenger.Transform].Move(passenger.Velocity, passenger.StandingOnPlatform);
			}
		}

		private void CalculatePassengerMovement(Vector3 velocity) {
			var movedPassengers = new HashSet<Transform> ();
			_passengerMovement = new List<PassengerMovement> ();

			var directionX = Mathf.Sign (velocity.x);
			var directionY = Mathf.Sign (velocity.y);

			// Vertically moving platform
			if (Math.Abs(velocity.y) > Mathf.Epsilon) {
				var rayLength = Mathf.Abs (velocity.y) + SkinWidth;
			
				for (var i = 0; i < verticalRayCount; i ++) {
					var rayOrigin = Math.Abs(directionY - (-1)) < Mathf.Epsilon?raycastOrigins.BottomLeft:raycastOrigins.TopLeft;
					rayOrigin += Vector2.right * (verticalRaySpacing * i);
					var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

					if (hit)
						if (!movedPassengers.Contains(hit.transform)) {
							movedPassengers.Add(hit.transform);
							var pushX = Math.Abs(directionY - 1) < Mathf.Epsilon?velocity.x:0;
							var pushY = velocity.y - (hit.distance - SkinWidth) * directionY;

							_passengerMovement.Add(new PassengerMovement(hit.transform,new Vector3(pushX,pushY), Math.Abs(directionY - 1) < Mathf.Epsilon, true));
						}
				}
			}

			// Horizontally moving platform
			if (Math.Abs(velocity.x) > Mathf.Epsilon) {
				var rayLength = Mathf.Abs (velocity.x) + SkinWidth;
			
				for (var i = 0; i < horizontalRayCount; i ++) {
					var rayOrigin = Math.Abs(directionX - (-1)) < Mathf.Epsilon?raycastOrigins.BottomLeft:raycastOrigins.BottomRight;
					rayOrigin += Vector2.up * (horizontalRaySpacing * i);
					var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

					if (hit)
						if (!movedPassengers.Contains(hit.transform)) {
							movedPassengers.Add(hit.transform);
							var pushX = velocity.x - (hit.distance - SkinWidth) * directionX;
							var pushY = -SkinWidth;
						
							_passengerMovement.Add(new PassengerMovement(hit.transform,new Vector3(pushX,pushY), false, true));
						}
				}
			}

			// Passenger on top of a horizontally or downward moving platform
			if (Math.Abs(directionY - (-1)) < Mathf.Epsilon || Math.Abs(velocity.y) < Mathf.Epsilon && Math.Abs(velocity.x) > Mathf.Epsilon) {
				const float rayLength = SkinWidth * 2;

				for (var i = 0; i < verticalRayCount; i ++) {
					var rayOrigin = raycastOrigins.TopLeft + Vector2.right * (verticalRaySpacing * i);
					var hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				
					if (hit)
						if (!movedPassengers.Contains(hit.transform)) {
							movedPassengers.Add(hit.transform);
							var pushX = velocity.x;
							var pushY = velocity.y;
						
							_passengerMovement.Add(new PassengerMovement(hit.transform,new Vector3(pushX,pushY), true, false));
						}
				}
			}
		}

		private struct PassengerMovement {
			public readonly Transform Transform;
			public readonly Vector3 Velocity;
			public readonly bool StandingOnPlatform;
			public readonly bool MoveBeforePlatform;

			public PassengerMovement(Transform transform, Vector3 velocity, bool standingOnPlatform, bool moveBeforePlatform) {
				Transform = transform;
				Velocity = velocity;
				StandingOnPlatform = standingOnPlatform;
				MoveBeforePlatform = moveBeforePlatform;
			}
		}

		private void OnDrawGizmos() {
			if (localWaypoints != null) {
				Gizmos.color = Color.red;
				const float size = .3f;

				for (var i =0; i < localWaypoints.Length; i ++) {
					var globalWaypointPos = Application.isPlaying?_globalWaypoints[i] : localWaypoints[i] + transform.position;
					Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
					Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
				}
			}
		}
	
	}
}