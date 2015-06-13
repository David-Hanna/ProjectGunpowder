using UnityEngine;
using System.Collections;

public class SoldierBehaviour : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;

	private Rigidbody2D rigidbody;

	void Start () {
	
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update() {

		if (rigidbody.velocity.sqrMagnitude > 0.0f) {
			
			// rotation
			float angle = (Mathf.Atan2 (rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg) - 90.0f;
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, angle), Time.fixedDeltaTime * 100);
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
