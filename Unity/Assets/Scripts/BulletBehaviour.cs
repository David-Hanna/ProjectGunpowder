using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider) {

		Destroy (gameObject);
	}
}
