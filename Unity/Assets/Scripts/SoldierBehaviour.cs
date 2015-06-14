using UnityEngine;
using System.Collections;

public class SoldierBehaviour : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;

	public float bulletSpeed;
	public float fireDelay;

	private Rigidbody2D rigidbody;
	private CountdownTimer fireDelayTimer;

	void Start () {
	
		rigidbody = GetComponent<Rigidbody2D> ();
		fireDelayTimer = new CountdownTimer ();
	}

	void Update() {

		if (rigidbody.velocity.sqrMagnitude > 0.0f) {
			
			// rotation
			float angle = (Mathf.Atan2 (rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg) - 90.0f;
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, angle), Time.fixedDeltaTime * 100);
		}

		if (fireDelayTimer.done) {

			if (Input.GetButton ("Fire")) {

				GameObject bullet = GameObject.Instantiate (Resources.Load ("Prefabs/Prototype/Weapons/Bullet")) as GameObject;
				bullet.transform.position = transform.GetChild (0).position;
				bullet.transform.rotation = transform.rotation * Quaternion.Euler (0, 0, 90);

				float angleInRads = bullet.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
				bullet.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletSpeed * Mathf.Cos (angleInRads), bulletSpeed * Mathf.Sin (angleInRads));
				fireDelayTimer.Start (fireDelay);
			}
		}
		else {
			fireDelayTimer.Update (Time.deltaTime);
		}
	}

	void FixedUpdate () {
	
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		// player input
		if (horizontal != 0 || vertical != 0) {

			Vector2 inputForce = new Vector2 (horizontal, vertical).normalized;
			inputForce *= acceleration * Time.fixedDeltaTime;

			rigidbody.AddForce (inputForce, ForceMode2D.Impulse);

			if (rigidbody.velocity.sqrMagnitude > maxSpeed) {

				rigidbody.velocity.Normalize ();
				rigidbody.velocity *= maxSpeed;
			}
		}
	}
}
