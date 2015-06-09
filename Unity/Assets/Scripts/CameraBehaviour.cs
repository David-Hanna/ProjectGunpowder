using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public float moveSpeed;
	public bool active;

	void FixedUpdate () {

		if (active) {

			float horizontal = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
			float vertical = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;

			GetComponent<Rigidbody>().velocity = new Vector3 (horizontal, vertical, 0.0f);
		}
	}
}
