using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {

	public GameObject dialogueBox;
	public DialogueBoxBehaviour dialogue { get; private set; }

	// Use this for initialization
	void Start () {

		dialogue = dialogueBox.GetComponent<DialogueBoxBehaviour> ();
	}
}
