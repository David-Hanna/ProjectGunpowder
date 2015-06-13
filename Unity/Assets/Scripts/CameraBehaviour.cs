using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public float dampTime = 0.15f;
	public Transform objectToFollow;

	private Vector3 velocity = Vector3.zero;

	void Update () {

		if (objectToFollow != null) {
			Vector3 targetPosition = new Vector3 (objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, dampTime);
		}
	}
}
