using UnityEngine;
using System.Collections;

public class DialogueBoxBehaviour : MonoBehaviour {
	
	private GameObject dialogueBox;

	private const float xOffset = 0.0f;
	private const float yOffset = -3.4f;
	private const float zPosition = -9.0f;

	private const float xScale = 10.0f;
	private const float yScale = 1.3f;
	private const float zScale = 1.0f;

	void Start () {
	
		GameObject mainCamera = GameObject.FindWithTag ("MainCamera");

		dialogueBox = this.gameObject;
		dialogueBox.transform.position = new Vector3 (mainCamera.transform.position.x + xOffset, mainCamera.transform.position.y + yOffset, zPosition);
		dialogueBox.transform.localScale = new Vector3 (xScale, yScale, zScale);
	}

//	void Update () {
//	
//	}

	public void SetText() {

		ClearText ();
		// ...
	}

	public void SetText(float revealSpeed) {
	
		ClearText ();
		// ...
	}

	public void ClearText() {

		// ...
	}
}
