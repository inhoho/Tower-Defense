using UnityEngine;
using System.Collections;


public class Game : MonoBehaviour {
	
	
	public GUIStyle towerButton;
	public GUIStyle totemButton;
	
	public Texture2D defenseWindow;
	public Texture2D scoreWindow;
	public Texture2D wood;
	public Texture2D health;
	
	public GameObject tribeNormalPrefab;
	public GameObject tribeHeavyPrefab;
	public GameObject towerObject;
	public GameObject totemObject;
	public int inc = 0;
	public float baseHealth = 50f;
	public int enemiesLeft;
	public int playerScore = 0;
	public int playersWood = 200;

	public int stage=1;
	// money
	public int money=0;


	private Vector3 startPos;
	private float spawnTime;
	private float spawnTimeLeft;
	private float spawnTimeSeconds = 2f;
	private int i = 0;
	private int[] waveFormation;
	private int towerCost = 50;
	private int totemCost = 100;
	private string objectToPlaceName;
	private RaycastHit hit;
	private Vector3 placementPosition;

	public int exe;
	
	void Awake()
	{
		spawnTime = Time.time;
	
	}
	
	void Start () {
		exe=PlayerPrefs.GetInt ("exec");
		if (exe != 0) {
			playerScore = PlayerPrefs.GetInt("SCORE");
			stage = PlayerPrefs.GetInt ("STAGE");

		}
		exe++;
		PlayerPrefs.SetInt ("exec", exe);
		PlayerPrefs.Save ();
		startPos = new Vector3(-2.6f, 0, -3.1f);

		waveFormation = new int[] {1, 0, 0, 0, 1, 0, 1, 1};
		enemiesLeft = waveFormation.Length;
	}
	
	void Update () 
	{
				spawnTimeLeft = Time.time - spawnTime;
				if (spawnTimeLeft >= spawnTimeSeconds) {
						if (i != waveFormation.Length) {
								if (waveFormation [i] == 0) {
					
										Instantiate (tribeNormalPrefab, startPos, Quaternion.identity);
										spawnTime = Time.time;
										spawnTimeLeft = 0;
										i++;
								} else {
					
										Instantiate (tribeHeavyPrefab, startPos, Quaternion.identity);
										spawnTime = Time.time;
										spawnTimeLeft = 0;
										i++;
								}
						}
				}
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
						if (Input.GetMouseButtonDown (0)) {
								if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
										if (hit.collider.tag == "yogi") {
												if (objectToPlaceName == "tower") {
														playersWood = playersWood - towerCost;
														placementPosition = hit.transform.position;
														GameObject arrowTower = (GameObject)Instantiate (towerObject,
						                                placementPosition, Quaternion.identity);
														arrowTower.name = inc.ToString ();
														Destroy (hit.collider.gameObject);
														objectToPlaceName = "";
														audio.Play ();
												}
					
												if (objectToPlaceName == "totem") {
														playersWood = playersWood - totemCost;
														placementPosition = hit.transform.position;
														GameObject totem = (GameObject)Instantiate (totemObject,
						                                 placementPosition, Quaternion.identity);
														totem.transform.Rotate (0, 180, 0);
														totem.name = inc.ToString ();
														Destroy (hit.collider.gameObject);
														objectToPlaceName = "";
														audio.Play ();
												}
						
										}
								}
						}
		
				}
		
	void OnGUI()
	{
		GUIStyle guiStyle = new GUIStyle();
		guiStyle.fontSize = 70;
		guiStyle.normal.textColor = Color.white;
		
		GUI.Label(new Rect(0, -3, 103, 317), defenseWindow);
		GUI.Label(new Rect(105, -3, 328, 64), scoreWindow);
		GUI.Label(new Rect(10, 23, 126, 98), wood);
		GUI.Label(new Rect(115, 0, 82, 68), health);
		GUI.Label(new Rect(85, 80, 100, 30), playersWood.ToString());
		GUI.Label(new Rect(160, 30, 100, 30), baseHealth.ToString());
		GUI.Label(new Rect(266, 31, 100, 30), playerScore.ToString());
	//	GUI.Label(new Rect(266, 51, 100, 30), stage.ToString());

		if (GUI.Button(new Rect(10, 135, 126, 98), "", towerButton))
		{
			if (playersWood >= towerCost)
			{
				objectToPlaceName = "tower";
			}
		}
		
		if (GUI.Button(new Rect(10, 225, 126, 98), "", totemButton))
		{
			if (playersWood >= totemCost)
			{
				objectToPlaceName = "totem";
			}
		}
		
		GUI.Label(new Rect(85, 190, 100, 30), "x50");
		GUI.Label(new Rect(85, 280, 100, 30), "x100");
		
		
		if (baseHealth == 0)
		{
			GUI.Label(new Rect(260, 260, 400, 100), "Game Over!", guiStyle);
			GUI.Label(new Rect(280, 340, 400, 100), "Press mouse button to play again");
			if (Input.GetMouseButtonDown(0))
				Application.LoadLevel(0);
		}
		else if (enemiesLeft == 0)
		{	

			GUI.Label(new Rect(260, 260, 400, 100), "You Win!", guiStyle);
			GUI.Label(new Rect(280, 340, 400, 100), "Press mouse button to play again");
			if (Input.GetMouseButtonDown(0)){
				stage++;
				PlayerPrefs.SetInt("SCORE",playerScore);
				PlayerPrefs.Save();
				PlayerPrefs.SetInt("STAGE",stage);
				PlayerPrefs.Save();
				if(stage>=4)
				{
					Application.LoadLevel(6);
				}
					Application.LoadLevel(5);
			}
		}
		
		
	}
}
