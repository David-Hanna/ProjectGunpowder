using System.Xml;
using UnityEngine;
using System.Collections.Generic;

//-----------------------------------------------------------------
// Handles Game Strings.
//		- Loads strings from GameStrings.xml.
//		- Transforms a StringID into a language-specific value.
//
// NOTES:
//		- Call LoadStrings() and before using.
//-----------------------------------------------------------------

//--------------------
// LANGUAGE SETTINGS
//--------------------
public class Language {

	public string Value { get; private set; }

	private Language(string lang) {

		Value = lang;
	}

	public static Language English() {

		return new Language ("English");
	}
}

public class StringTool {

	public Language selectedLanguage { get; private set; }
	private Dictionary<string, string> loadedStrings;

	public StringTool() {

		selectedLanguage = Language.English ();
		loadedStrings = new Dictionary<string, string> ();
	}

	public StringTool(Language lang) {

		selectedLanguage = lang;
		loadedStrings = new Dictionary<string, string>();
	}

	public void ChangeLanguage(Language lang) {

		selectedLanguage = lang;
		LoadStrings ();
	}

	public void LoadStrings() {

		loadedStrings.Clear();

		try {
			TextAsset gameStringsTxt = (TextAsset)Resources.Load ("GameStrings");

			XmlDocument gameStringsDoc = new XmlDocument();
			gameStringsDoc.LoadXml (gameStringsTxt.text);

			XmlNodeList stringIDNodes = gameStringsDoc.SelectNodes ("GameStrings/String");

			foreach (XmlNode stringIDNode in stringIDNodes) {

				string stringID = stringIDNode.Attributes["id"].Value;

				XmlNode stringValueNode = stringIDNode.SelectSingleNode (selectedLanguage.Value);
				loadedStrings.Add (stringID, stringValueNode.Attributes["value"].Value);
			}
		}
		catch {

			Debug.LogError ("------------------------------\nFAILED TO LOAD GAME STRINGS\n------------------------------");
		}
	}

	public string GetStringByID(string id) {

		if (loadedStrings.ContainsKey (id)) {

			return loadedStrings [id];
		} 
		else {

			Debug.LogError ("StringID not found: " + id);
			return "";
		}
	}
}
