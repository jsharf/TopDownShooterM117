using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width * 0.25f, Screen.height * 0.25f,
		                      Screen.width * 0.5f, Screen.height * 0.1f), "Play Game")) {
			Application.LoadLevel ("game");
		}
	}	
}
