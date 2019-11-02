using System;
using UnityEngine;

namespace Player {
	public class PlayerController : PlayerRaycast {

	public float maxSlopeAngle = 80;

	public CollisionInfo collisions;
	[HideInInspector]
	public Vector2 playerInput;

	public override void Start() {
		base.Start ();
		collisions.FaceDir = 1;

	}

	public void Move(Vector2 moveAmount, bool standingOnPlatform) {
		Move (moveAmount, Vector2.zero, standingOnPlatform);
	}

	public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();

		collisions.Reset ();
		collisions.MoveAmountOld = moveAmount;
		playerInput = input;

		if (moveAmount.y < 0) {
			DescendSlope(ref moveAmount);
		}

		if (Math.Abs(moveAmount.x) > Mathf.Epsilon) {
			collisions.FaceDir = (int)Mathf.Sign(moveAmount.x);
		}

		HorizontalCollisions (ref moveAmount);
		if (Math.Abs(moveAmount.y) > Mathf.Epsilon) {
			VerticalCollisions (ref moveAmount);
		}

		transform.Translate (moveAmount);

		if (standingOnPlatform) {
			collisions.Below = true;
		}
	}

	private void HorizontalCollisions(ref Vector2 moveAmount) {
		float directionX = collisions.FaceDir;
		var rayLength = Mathf.Abs (moveAmount.x) + SkinWidth;

		if (Mathf.Abs(moveAmount.x) < SkinWidth) {
			rayLength = 2*SkinWidth;
		}

		for (var i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (Math.Abs(directionX - (-1)) < Mathf.Epsilon)?raycastOrigins.BottomLeft:raycastOrigins.BottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX,Color.red);

			if (hit) {

				if (Math.Abs(hit.distance) < Mathf.Epsilon) {
					continue;
				}

				var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxSlopeAngle) {
					if (collisions.DescendingSlope) {
						collisions.DescendingSlope = false;
						moveAmount = collisions.MoveAmountOld;
					}
					float distanceToSlopeStart = 0;
					if (Math.Abs(slopeAngle - collisions.SlopeAngleOld) > Mathf.Epsilon) {
						distanceToSlopeStart = hit.distance-SkinWidth;
						moveAmount.x -= distanceToSlopeStart * directionX;
					}
					ClimbSlope(ref moveAmount, slopeAngle, hit.normal);
					moveAmount.x += distanceToSlopeStart * directionX;
				}

				if (!collisions.ClimbingSlope || slopeAngle > maxSlopeAngle) {
					moveAmount.x = (hit.distance - SkinWidth) * directionX;
					rayLength = hit.distance;

					if (collisions.ClimbingSlope) {
						moveAmount.y = Mathf.Tan(collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
					}
				}
			}
		}
	}

	private void VerticalCollisions(ref Vector2 moveAmount) {
		var directionY = Mathf.Sign (moveAmount.y);
		var rayLength = Mathf.Abs (moveAmount.y) + SkinWidth;

		for (var i = 0; i < verticalRayCount; i ++) {

			var rayOrigin = (Math.Abs(directionY - (-1)) < Mathf.Epsilon)?raycastOrigins.BottomLeft:raycastOrigins.TopLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY,Color.red);

			if (hit) {
				if (hit.collider.CompareTag("Through")) {
					if (Math.Abs(directionY - 1) < Mathf.Epsilon || Math.Abs(hit.distance) < Mathf.Epsilon) {
						continue;
					}
					if (collisions.FallingThroughPlatform) {
						continue;
					}
					if (Math.Abs(playerInput.y - (-1)) < Mathf.Epsilon) {
						collisions.FallingThroughPlatform = true;
						Invoke(nameof(ResetFallingThroughPlatform),.5f);
						continue;
					}
				}

				moveAmount.y = (hit.distance - SkinWidth) * directionY;
				rayLength = hit.distance;

				if (collisions.ClimbingSlope) {
					moveAmount.x = moveAmount.y / Mathf.Tan(collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
				}

				collisions.Below = Math.Abs(directionY - (-1)) < Mathf.Epsilon;
				collisions.Above = Math.Abs(directionY - 1) < Mathf.Epsilon;
			}
		}

		if (collisions.ClimbingSlope) {
			var directionX = Mathf.Sign(moveAmount.x);
			rayLength = Mathf.Abs(moveAmount.x) + SkinWidth;
			var rayOrigin = ((Math.Abs(directionX - (-1)) < Mathf.Epsilon)?raycastOrigins.BottomLeft:raycastOrigins.BottomRight) + Vector2.up * moveAmount.y;
			var hit = Physics2D.Raycast(rayOrigin,Vector2.right * directionX,rayLength,collisionMask);

			if (hit) {
				var slopeAngle = Vector2.Angle(hit.normal,Vector2.up);
				if (Math.Abs(slopeAngle - collisions.SlopeAngle) > Mathf.Epsilon) {
					moveAmount.x = (hit.distance - SkinWidth) * directionX;
					collisions.SlopeAngle = slopeAngle;
					collisions.SlopeNormal = hit.normal;
				}
			}
		}
	}

	private void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal) {
		var moveDistance = Mathf.Abs (moveAmount.x);
		var climbmoveAmountY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (moveAmount.y <= climbmoveAmountY) {
			moveAmount.y = climbmoveAmountY;
			moveAmount.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (moveAmount.x);
			collisions.Below = true;
			collisions.ClimbingSlope = true;
			collisions.SlopeAngle = slopeAngle;
			collisions.SlopeNormal = slopeNormal;
		}
	}

	void DescendSlope(ref Vector2 moveAmount) {

		RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast (raycastOrigins.BottomLeft, Vector2.down, Mathf.Abs (moveAmount.y) + SkinWidth, collisionMask);
		RaycastHit2D maxSlopeHitRight = Physics2D.Raycast (raycastOrigins.BottomRight, Vector2.down, Mathf.Abs (moveAmount.y) + SkinWidth, collisionMask);
		if (maxSlopeHitLeft ^ maxSlopeHitRight) {
			SlideDownMaxSlope (maxSlopeHitLeft, ref moveAmount);
			SlideDownMaxSlope (maxSlopeHitRight, ref moveAmount);
		}

		if (!collisions.SlidingDownMaxSlope) {
			var directionX = Mathf.Sign (moveAmount.x);
			var rayOrigin = (Math.Abs(directionX - (-1)) < Mathf.Epsilon) ? raycastOrigins.BottomRight : raycastOrigins.BottomLeft;
			var hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

			if (hit) {
				var slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
				if (Math.Abs(slopeAngle) > Mathf.Epsilon && slopeAngle <= maxSlopeAngle) {
					if (Math.Abs(Mathf.Sign (hit.normal.x) - directionX) < Mathf.Epsilon) {
						if (hit.distance - SkinWidth <= Mathf.Tan (slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (moveAmount.x)) {
							var moveDistance = Mathf.Abs (moveAmount.x);
							var descendmoveAmountY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;
							moveAmount.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign (moveAmount.x);
							moveAmount.y -= descendmoveAmountY;

							collisions.SlopeAngle = slopeAngle;
							collisions.DescendingSlope = true;
							collisions.Below = true;
							collisions.SlopeNormal = hit.normal;
						}
					}
				}
			}
		}
	}

	private void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount) {

		if (hit) {
			var slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if (slopeAngle > maxSlopeAngle) {
				moveAmount.x = Mathf.Sign(hit.normal.x) * (Mathf.Abs (moveAmount.y) - hit.distance) / Mathf.Tan (slopeAngle * Mathf.Deg2Rad);

				collisions.SlopeAngle = slopeAngle;
				collisions.SlidingDownMaxSlope = true;
				collisions.SlopeNormal = hit.normal;
			}
		}

	}

	void ResetFallingThroughPlatform() {
		collisions.FallingThroughPlatform = false;
	}

	public struct CollisionInfo {
		public bool Above, Below;

		public bool ClimbingSlope;
		public bool DescendingSlope;
		public bool SlidingDownMaxSlope;

		public float SlopeAngle, SlopeAngleOld;
		public Vector2 SlopeNormal;
		public Vector2 MoveAmountOld;
		public int FaceDir;
		public bool FallingThroughPlatform;

		public void Reset() {
			Above = Below = false;
			ClimbingSlope = false;
			DescendingSlope = false;
			SlidingDownMaxSlope = false;
			SlopeNormal = Vector2.zero;

			SlopeAngleOld = SlopeAngle;
			SlopeAngle = 0;
		}
	}

}

	public class PlayerRaycast : MonoBehaviour {

		public LayerMask collisionMask;

		protected const float SkinWidth = .015f;
		[HideInInspector]
		public int horizontalRayCount;
		[HideInInspector]
		public int verticalRayCount;

		[HideInInspector]
		public float horizontalRaySpacing;
		[HideInInspector]
		public float verticalRaySpacing;

		[HideInInspector]
		public new BoxCollider2D collider;
		public RaycastOrigins raycastOrigins;

		private const float DistanceBetweenRays = .25f;
		
		
		public virtual void Start() {
			collider = GetComponent<BoxCollider2D> ();
			CalculateRaySpacing ();
		}

		protected void UpdateRaycastOrigins() {
			var bounds = collider.bounds;
			bounds.Expand (SkinWidth * -2);
		
			raycastOrigins.BottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
			raycastOrigins.BottomRight = new Vector2 (bounds.max.x, bounds.min.y);
			raycastOrigins.TopLeft = new Vector2 (bounds.min.x, bounds.max.y);
			// ReSharper disable once ObjectCreationAsStatement
			new Vector2 (bounds.max.x, bounds.max.y);
		}

		private void CalculateRaySpacing() {
			var bounds = collider.bounds;
			bounds.Expand (SkinWidth * -2);

			var boundsWidth = bounds.size.x;
			var boundsHeight = bounds.size.y;

			horizontalRayCount = Mathf.RoundToInt(boundsHeight / DistanceBetweenRays);
			verticalRayCount = Mathf.RoundToInt(boundsWidth / DistanceBetweenRays);
		
			horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
		}
	
		public struct RaycastOrigins {
			public Vector2 TopLeft;
			public Vector2 BottomLeft, BottomRight;
		}
	}
}