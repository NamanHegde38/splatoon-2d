using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Player  {
	[RequireComponent(typeof (BoxCollider2D))]
	[SuppressMessage("ReSharper", "NotAccessedField.Global")]
	public class PlayerController : MonoBehaviour {

		public LayerMask collisionMask;

		private const float SkinWidth = .015f;
		public int horizontalRayCount = 4;
		public int verticalRayCount = 4;

		private const float MaxClimbAngle = 60;
		private const float MaxDescendAngle = 60;

		private float _horizontalRaySpacing;
		private float _verticalRaySpacing;

		private BoxCollider2D _boxCollider;
		private RaycastOrigins _raycastOrigins;
		public CollisionInfo collisions;

		private void Start () {
			SetComponents();
			CalculateRaySpacing();
		}

		private void SetComponents() {
			_boxCollider = GetComponent<BoxCollider2D>();
		}

		public void Move (Vector3 velocity) {

			UpdateRaycastOrigins();
			collisions.Reset();
			collisions.VelocityOld = velocity;

			if (velocity.y < 0) {
				DescendSlope(ref velocity);
			}

			if (velocity.x > 0) {
				HorizontalCollisions(ref velocity);
			}

			if (velocity.y != 0) {
				VerticalCollisions(ref velocity);
			}

			transform.Translate(velocity);
		}

		private void HorizontalCollisions (ref Vector3 velocity) {

			var directionX = Mathf.Sign(velocity.x);
			var rayLength = Mathf.Abs(velocity.x) + SkinWidth;

			var rayOrigin = (directionX == -1) ? _raycastOrigins.BottomLeft : _raycastOrigins.BottomRight;
			for (var i = 0; i < horizontalRayCount; i++) {
				rayOrigin += Vector2.up * (_horizontalRaySpacing * i);
				var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				Debug.DrawRay(rayOrigin, directionX * rayLength * Vector2.right, Color.red);

				if (!hit) {
					continue;
				}

				var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= MaxClimbAngle) {
					if (slopeAngle != collisions.SlopeAngleOld) {
						var distanceToSlopeStart = hit.distance - SkinWidth;
						velocity.x -= distanceToSlopeStart * directionX;
					}

					ClimbSlope(ref velocity, slopeAngle);
				}

				if (collisions.ClimbingSlope && !(slopeAngle > MaxClimbAngle)) {
					continue;
				}

				velocity.x = (hit.distance - SkinWidth) * directionX;
				rayLength = hit.distance;

				if (collisions.ClimbingSlope) {
					velocity.y = Mathf.Tan(collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
				}

				collisions.Left = directionX == -1;
				collisions.Right = directionX == 1;
			}
		}

		private void VerticalCollisions (ref Vector3 velocity) {

			var directionY = Mathf.Sign(velocity.y);
			var rayLength = Mathf.Abs(velocity.y) + SkinWidth;

			for (var i = 0; i < verticalRayCount; i++) {

				var rayOrigin = (directionY == -1) ? _raycastOrigins.BottomLeft : _raycastOrigins.TopLeft;
				rayOrigin += Vector2.right * (_verticalRaySpacing * i + velocity.x);
				var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

				Debug.DrawRay(rayOrigin, directionY * rayLength * Vector2.up, Color.red);

				if (!hit) {
					continue;
				}

				velocity.y = (hit.distance - SkinWidth) * directionY;
				rayLength = hit.distance;

				if (collisions.ClimbingSlope) {
					velocity.x = velocity.y / Mathf.Tan(collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
				}

				collisions.Below = directionY == -1;
				collisions.Above = directionY == 1;
			}

			if (!collisions.ClimbingSlope) {
				return;
			}

			{
				var directionX = Mathf.Sign(velocity.x);
				rayLength = Mathf.Abs(velocity.x) + SkinWidth;
				var rayOrigin = ((directionX == -1) ? _raycastOrigins.BottomLeft : _raycastOrigins.BottomRight) + Vector2.up * velocity.y;
				var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				if (!hit) {
					return;
				}

				var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if (slopeAngle != collisions.SlopeAngle) {
					velocity.x = (hit.distance - SkinWidth) * directionX;
					collisions.SlopeAngle = slopeAngle;
				}
			}
		}

		private void ClimbSlope (ref Vector3 velocity, float slopeAngle) {

			var moveDistance = Mathf.Abs(velocity.x);
			var climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

			if (!(velocity.y <= climbVelocityY)) {
				return;
			}

			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
			collisions.Below = true;
			collisions.ClimbingSlope = true;
			collisions.SlopeAngle = slopeAngle;
		}

		private void DescendSlope (ref Vector3 velocity) {

			var directionX = Mathf.Sign(velocity.x);
			var rayOrigin = (directionX == -1) ? _raycastOrigins.BottomRight : _raycastOrigins.BottomLeft;
			var hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

			if (!hit) {
				return;
			}

			var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if (slopeAngle == 0 || !(slopeAngle <= MaxDescendAngle)) {
				return;
			}

			if (Mathf.Sign(hit.normal.x) != directionX) {
				return;
			}

			if (!(hit.distance - SkinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))) {
				return;
			}

			var moveDistance = Mathf.Abs(velocity.x);
			var descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
			velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
			velocity.y -= descendVelocityY;

			collisions.SlopeAngle = slopeAngle;
			collisions.DescendingSlope = true;
			collisions.Below = true;
		}
		
		private void UpdateRaycastOrigins () {

			var bounds = _boxCollider.bounds;
			bounds.Expand(SkinWidth * -2);

			_raycastOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
			_raycastOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
			_raycastOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
			_raycastOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
		}

		private void CalculateRaySpacing () {

			var bounds = _boxCollider.bounds;
			bounds.Expand(SkinWidth * -2);

			horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
			verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

			_horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			_verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
		}

		private struct RaycastOrigins {

			// ReSharper disable once NotAccessedField.Local
			public Vector2 TopLeft, TopRight;
			public Vector2 BottomLeft, BottomRight;
		}

		public struct CollisionInfo {

			public bool Above, Below;
			public bool Left, Right;

			public bool ClimbingSlope;
			public bool DescendingSlope;
			public float SlopeAngle, SlopeAngleOld;
			public Vector3 VelocityOld;

			public void Reset () {

				Above = Below = false;
				Left = Right = false;
				ClimbingSlope = false;
				DescendingSlope = false;

				SlopeAngleOld = SlopeAngle;
				SlopeAngle = 0;
			}
		}
	}
}