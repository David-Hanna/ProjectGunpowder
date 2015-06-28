using UnityEngine;
using System.Collections;

public class RifleBulletBehaviour : MonoBehaviour {

	public float speed { get; set; }
	public float maxDistanceSquared { get; set; }
	private Vector3 initialPosition;

	void Start () {
	
		initialPosition = transform.position;

		float angleInRads = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * Mathf.Cos (angleInRads), speed * Mathf.Sin (angleInRads));
	}

	void Update () {

		float distanceTravelledSquared = (transform.position - initialPosition).sqrMagnitude;
		if (distanceTravelledSquared > maxDistanceSquared) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy (gameObject);
	}
}
