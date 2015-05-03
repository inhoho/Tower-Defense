using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {
	public GUIStyle menuButton1;
	public GUIStyle menuButton2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(80, 400, 100, 40), "", menuButton1))
		{
			Application.LoadLevel(5);
		}
		if (GUI.Button(new Rect(210, 400, 100, 40), "", menuButton2))
		{
			Application.LoadLevel(6);
		}
	}
}
