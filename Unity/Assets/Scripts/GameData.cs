using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

	public StringTool stringTool { get; private set; }

	// Use this for initialization
	void Start () {
	
		stringTool = new StringTool ();
		stringTool.LoadStrings ();
	}
}
