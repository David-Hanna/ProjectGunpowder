using UnityEngine;
using System.Collections;

public class DialogueBoxBehaviour : MonoBehaviour {

	public Font font;

	private int displayUpToIndex;
	private float secondsPerLetter;
	private float revealHelper;
	private Rect textBox;
	private Rect textSpace;
	private Texture2D texture;
	private GUIStyle boxStyle;
	private GUIStyle textStyle;
	private StringTool stringTool;

	public bool active { get; private set; }
	public string text { get; private set; }

	public const float REVEAL_SPEED_SLOW = 0.10f;
	public const float REVEAL_SPEED_MEDIUM = 0.04f;
	public const float REVEAL_SPEED_FAST = 0.01f;
	public const float REVEAL_SPEED_INSTANT = 0.0000001f;

	void Start () {

		displayUpToIndex = 0;
		secondsPerLetter = REVEAL_SPEED_MEDIUM;
		revealHelper = 0.0f;

		textBox = new Rect
			(
				transform.position.x + (Screen.width * 0.03125f),
				transform.position.y + (Screen.height * 0.82f),
				Screen.width * 0.6625f,
				Screen.height - transform.position.y
			);

		textSpace = new Rect
			(
				transform.position.x + (Screen.width * 0.0625f),
			 	transform.position.y + (Screen.height * 0.81f),
			 	Screen.width * 0.6f,
			 	Screen.height
			);

		texture = new Texture2D (1, 1);
		texture.SetPixel (0, 0, new Color(0, 0, 1.0f, 0.5f));
		texture.Apply ();

		boxStyle = new GUIStyle ();
		boxStyle.normal.background = texture;

		textStyle = new GUIStyle ();
		textStyle.font = font;
		textStyle.normal.textColor = font.material.color;
		textStyle.wordWrap = true;

		active = false;
	}

	void Update () {

		if (Input.GetButtonDown ("Jump")) {
			SetActive (!active);
		}
	
		if (active && displayUpToIndex < text.Length) {

			if (Input.GetKey (KeyCode.Space)) {

				displayUpToIndex = text.Length;
			}

			revealHelper += Time.deltaTime;

			if (revealHelper > secondsPerLetter) {

				displayUpToIndex += (int)(revealHelper / secondsPerLetter);

				if (displayUpToIndex >= text.Length) {
					displayUpToIndex = text.Length;
				}
				else {
					revealHelper %= secondsPerLetter;
				}
			}
		}
	}

	void OnGUI() {

		if (active) {

			GUI.Box (textBox, GUIContent.none, boxStyle);
			GUI.Label (textSpace, text.Substring (0, displayUpToIndex), textStyle);
		}
	}

	public void SetActive(bool _active) {

		active = _active;
		if (active) {
			stringTool = GameObject.FindGameObjectWithTag ("GameData").GetComponent<GameData>().stringTool;
			text = stringTool.GetStringByID ("level1_begin_line4");
		}
	}

	public void SetText(string _text) {

		SetText (_text, REVEAL_SPEED_MEDIUM);
	}

	public void SetText(string _text, float revealSpeed) {
	
		text = _text;
		displayUpToIndex = 0;
		secondsPerLetter = revealSpeed;
		revealHelper = 0.0f;
	}
}
