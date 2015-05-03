using UnityEngine;
using System.Collections;

public class MainMenu1 : MonoBehaviour {
	// Use this for initialization
	public int score;
	void Start () 
	{
		score = PlayerPrefs.GetInt ("SCORE");

	}
	
	// Update is called once per frame
	void Update () {

	}

	
	void OnGUI()
	{
		GUI.Label(new Rect(400, 300, 100, 50), score.ToString());
		GUI.Label (new Rect (300,300, 100,50), "Your Score : ");
		if (Input.GetMouseButtonDown (0)) {
			PlayerPrefs.DeleteAll();
			Application.LoadLevel (5);
		}
	}
}
