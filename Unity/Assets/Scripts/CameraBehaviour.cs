using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public float moveSpeed;

	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
	
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		float horizontal = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		float vertical = Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;

		rigidbody.velocity = new Vector3 (horizontal, vertical, 0.0f);
		//ownerTransform.Translate (new Vector3 (horizontal, vertical, 0.0f));

	}
}
