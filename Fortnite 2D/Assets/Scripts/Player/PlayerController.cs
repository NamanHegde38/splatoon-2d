using UnityEngine;

[RequireComponent(typeof (BoxCollider2D))]
public class PlayerController : MonoBehaviour {

	public LayerMask collisionMask;

	private const float _skinWidth = .015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	private const float _maxClimbAngle = 60;
	private const float _maxDescendAngle = 60;

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
		collisions.velocityOld = velocity;

		if (velocity.y < 0) {
			DescendSlope(ref velocity);
		}

		if (velocity.x != 0) {
			HorizontalCollisions(ref velocity);
		}

		if (velocity.y != 0) {
			VerticalCollisions(ref velocity);
		}

		transform.Translate(velocity);
	}

	private void HorizontalCollisions (ref Vector3 velocity) {

		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + _skinWidth;

		for (int i = 0; i < horizontalRayCount; i++) {

			Vector2 rayOrigin = (directionX == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (_horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, directionX * rayLength * Vector2.right, Color.red);

			if (!hit) {
				continue;
			}

			float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

			if (i == 0 && slopeAngle <= _maxClimbAngle) {
				if (slopeAngle != collisions.slopeAngleOld) {
					float distanceToSlopeStart = hit.distance - _skinWidth;
					velocity.x -= distanceToSlopeStart * directionX;
				}

				ClimbSlope(ref velocity, slopeAngle);
			}

			if (collisions.climbingSlope && !(slopeAngle > _maxClimbAngle)) {
				continue;
			}

			velocity.x = (hit.distance - _skinWidth) * directionX;
			rayLength = hit.distance;

			if (collisions.climbingSlope) {
				velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
			}

			collisions.left = directionX == -1;
			collisions.right = directionX == 1;
		}
	}

	private void VerticalCollisions (ref Vector3 velocity) {

		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + _skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {

			Vector2 rayOrigin = (directionY == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (_verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, directionY * rayLength * Vector2.up, Color.red);

			if (!hit) {
				continue;
			}

			velocity.y = (hit.distance - _skinWidth) * directionY;
			rayLength = hit.distance;

			if (collisions.climbingSlope) {
				velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
			}

			collisions.below = directionY == -1;
			collisions.above = directionY == 1;
		}

		if (!collisions.climbingSlope) {
			return;
		}

		{
			float directionX = Mathf.Sign(velocity.x);
			rayLength = Mathf.Abs(velocity.x) + _skinWidth;
			Vector2 rayOrigin = ((directionX == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (!hit) {
				return;
			}

			float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if (slopeAngle != collisions.slopeAngle) {
				velocity.x = (hit.distance - _skinWidth) * directionX;
				collisions.slopeAngle = slopeAngle;
			}
		}
	}

	private void ClimbSlope (ref Vector3 velocity, float slopeAngle) {

		float moveDistance = Mathf.Abs(velocity.x);
		float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (!(velocity.y <= climbVelocityY)) {
			return;
		}

		velocity.y = climbVelocityY;
		velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
		collisions.below = true;
		collisions.climbingSlope = true;
		collisions.slopeAngle = slopeAngle;
	}

	private void DescendSlope (ref Vector3 velocity) {

		float directionX = Mathf.Sign(velocity.x);
		Vector2 rayOrigin = (directionX == -1) ? _raycastOrigins.bottomRight : _raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

		if (!hit) {
			return;
		}

		float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
		if (slopeAngle == 0 || !(slopeAngle <= _maxDescendAngle)) {
			return;
		}

		if (Mathf.Sign(hit.normal.x) != directionX) {
			return;
		}

		if (!(hit.distance - _skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))) {
			return;
		}

		float moveDistance = Mathf.Abs(velocity.x);
		float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
		velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
		velocity.y -= descendVelocityY;

		collisions.slopeAngle = slopeAngle;
		collisions.descendingSlope = true;
		collisions.below = true;
	}

	private void UpdateRaycastOrigins () {

		Bounds bounds = _boxCollider.bounds;
		bounds.Expand(_skinWidth * -2);

		_raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		_raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		_raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		_raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	private void CalculateRaySpacing () {

		Bounds bounds = _boxCollider.bounds;
		bounds.Expand(_skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		_horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		_verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	private struct RaycastOrigins {

		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo {

		public bool above, below;
		public bool left, right;

		public bool climbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;

		public void Reset () {

			above = below = false;
			left = right = false;
			climbingSlope = false;
			descendingSlope = false;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}
}
