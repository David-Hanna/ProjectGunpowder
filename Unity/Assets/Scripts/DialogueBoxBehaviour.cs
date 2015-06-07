using UnityEngine;
using System.Collections;

public class DialogueBoxBehaviour : MonoBehaviour {

	public Font font;

	public bool active { get; set; }
	public string text { get; private set; }

	private int displayUpToIndex;
	private float secondsPerLetter;
	private float revealHelper;
	private Rect textBox;
	private Rect textSpace;
	private Texture2D texture;
	private GUIStyle boxStyle;
	private GUIStyle textStyle;

	public const float REVEAL_SPEED_SLOW = 0.10f;
	public const float REVEAL_SPEED_MEDIUM = 0.04f;
	public const float REVEAL_SPEED_FAST = 0.01f;
	public const float REVEAL_SPEED_INSTANT = 0.0000001f;

	void Start () {

		active = true;
		text = "Why hello there dialogue box! How are you today? I am great! Thank you! It is so wonderful that this text box is simply working so well, isn't it?\nTime to show off the third line by writing text here!\nIs a fourth line pushing it? Nope!";
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
		texture.SetPixel (0, 0, Color.blue);
		texture.Apply ();

		boxStyle = new GUIStyle ();
		boxStyle.normal.background = texture;

		textStyle = new GUIStyle ();
		textStyle.font = font;
		textStyle.normal.textColor = font.material.color;
		textStyle.wordWrap = true;
	}

	void Update () {
	
		if (active && displayUpToIndex < text.Length) {

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
