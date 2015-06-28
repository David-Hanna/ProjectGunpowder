using UnityEngine;
using System.Collections;

public class RifleBehaviour : MonoBehaviour {

	public bool playerControlled;

	public float acceleration;
	public float maxSpeed;

	public GameObject bullet;
	public float fireDelay;
	public float bulletSpeed;
	public float bulletMaxDistanceSquared;

	private Rigidbody2D rigidbody;
	private CountdownTimer fireDelayTimer;
	private AIState aiState;

	void Start () {
	
		rigidbody = GetComponent<Rigidbody2D> ();
		fireDelayTimer = new CountdownTimer ();
		aiState = new AIState_ChoosingTarget (this);
	}

	void Update() {

		if (!fireDelayTimer.done) {
			fireDelayTimer.Update (Time.deltaTime);
		}
		if (playerControlled) {
			if (Input.GetButton ("Fire")) {
				Fire ();
			}
		}
		else {
			aiState.Update ();
		}
	}

	void FixedUpdate () {
	
		if (playerControlled) {

			float horizontal = Input.GetAxisRaw ("Horizontal");
			float vertical = Input.GetAxisRaw ("Vertical");

			// player input
			if (horizontal != 0.0f || vertical != 0.0f) {

				Vector2 inputForce = new Vector2 (horizontal, vertical).normalized;
				inputForce *= acceleration * Time.fixedDeltaTime;
				Move (inputForce);
			}
		} 
		else {
			aiState.FixedUpdate ();
		}
	}

	private void Move(Vector2 force) {
		rigidbody.AddForce (force, ForceMode2D.Impulse);
		
		if (rigidbody.velocity.sqrMagnitude > maxSpeed) {
			
			rigidbody.velocity.Normalize ();
			rigidbody.velocity *= maxSpeed;
		}

		float angle = (Mathf.Atan2 (rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg) - 90.0f;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	private void Fire() {

		if (fireDelayTimer.done) {
			GameObject bulletFired = GameObject.Instantiate (bullet) as GameObject;
			bulletFired.transform.position = transform.GetChild (0).position;
			bulletFired.transform.rotation = transform.rotation * Quaternion.Euler (0, 0, 90);
			RifleBulletBehaviour bulletBehaviour = bulletFired.GetComponent<RifleBulletBehaviour> ();
			bulletBehaviour.speed = bulletSpeed;
			bulletBehaviour.maxDistanceSquared = bulletMaxDistanceSquared;
			fireDelayTimer.Start(fireDelay);
		}
	}
	
	private void ChangeState(AIState nextState) {

		aiState = nextState;
	}

	private abstract class AIState {

		public abstract void Update();
		public abstract void FixedUpdate();
	}

	private class AIState_ChoosingTarget : AIState {

		private RifleBehaviour owner;

		public AIState_ChoosingTarget(RifleBehaviour _owner) {

			owner = _owner;
		}

		public override void Update() {

			GameObject[] blueTeam = GameObject.FindGameObjectsWithTag ("BlueTeam");
			if (blueTeam.Length == 0)
				return;
			Transform closestTarget = blueTeam[0].transform;
			float squareClosestDistance = (owner.transform.position - closestTarget.position).sqrMagnitude;
			for (int i = 1; i < blueTeam.Length; i++) {
				float squareDistance = (owner.transform.position - blueTeam[i].transform.position).sqrMagnitude;
				if (squareDistance < squareClosestDistance)
					closestTarget = blueTeam[i].transform;
			}
			owner.ChangeState(new AIState_HuntingTarget(owner, closestTarget));
		}

		public override void FixedUpdate() {
		}
	}

	private class AIState_HuntingTarget : AIState {

		private RifleBehaviour owner;
		private Transform target;
		private float threeQuartersBulletMaxDistanceSquared;

		public AIState_HuntingTarget(RifleBehaviour _owner, Transform _target) {
			owner = _owner;
			target = _target;
			threeQuartersBulletMaxDistanceSquared = owner.bulletMaxDistanceSquared * 0.75f;
		}

		public override void Update() {
			if (target == null)
				owner.ChangeState (new AIState_ChoosingTarget (owner));

			float squareDistanceFromTarget = (owner.transform.position - target.position).sqrMagnitude;
			if (squareDistanceFromTarget < owner.bulletMaxDistanceSquared) {
				owner.Fire ();
			}
		}

		public override void FixedUpdate() {
			if (target == null)
				owner.ChangeState (new AIState_ChoosingTarget (owner));

			Vector2 towardTarget = target.position - owner.transform.position;
			float squareDistanceFromTarget = towardTarget.sqrMagnitude;
			if (squareDistanceFromTarget > threeQuartersBulletMaxDistanceSquared) {
				owner.Move (towardTarget.normalized * owner.acceleration * Time.fixedDeltaTime);
			}
			float angle = (Mathf.Atan2 (towardTarget.y, towardTarget.x) * Mathf.Rad2Deg) - 90.0f;
			owner.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}
}
