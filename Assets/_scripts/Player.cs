using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GUIStyle menuButton1;
	public GUIStyle menuButton2;
	public GUIStyle menuButton3;
	public GUIStyle menuButton4;
	int stage_width = 75;
	public int stage = 0;

	public Transform[] waypoints = new Transform[7];
	public Transform activeWaypoint;
	public int currentWaypoint = 0;
	public int objWaypoint=0;

	private int state = 0;
	private Transform myPos;
	private float accel = 30f;
	private float rotationDamping = 6.0f;
	
	private GameObject wp10bj;
	private GameObject wp20bj;
	private GameObject wp30bj;
	private GameObject wp40bj;
	private GameObject wp50bj;
	private GameObject wp60bj;
	public int stgae;

	void Start () 
	{
		stage=PlayerPrefs.GetInt ("STAGE");
		if (stage > 4) {
			Application.LoadLevel(6);
		}
		currentWaypoint = 0;
		state = 0;
		myPos = transform;
		
		wp10bj = GameObject.Find("waypoint_01");
		wp20bj = GameObject.Find("waypoint_02");
		wp30bj = GameObject.Find("waypoint_03");
		wp40bj = GameObject.Find("waypoint_04");
		wp50bj = GameObject.Find("waypoint_05");
		wp60bj = GameObject.Find("waypoint_06");
		
		waypoints[0] = wp10bj.transform;
		waypoints[1] = wp20bj.transform;
		waypoints[2] = wp30bj.transform;
		waypoints[3] = wp40bj.transform;
		waypoints[4] = wp50bj.transform;
		waypoints[5] = wp60bj.transform;
	}
	
	void Update () 
	{
		if (state == 0) {
				if (currentWaypoint != objWaypoint) {
							walk();
							activeWaypoint = waypoints [currentWaypoint];
				}
		}
//		Debug.DrawLine(myPos.position, activeWaypoint.position, Color.red);
	}
	
	void walk() 
	{
		Quaternion rotation = Quaternion.LookRotation(waypoints[currentWaypoint].position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
		Vector3 waypointDirection = waypoints[currentWaypoint].position - transform.position;
		float speedFactor = Vector3.Dot(waypointDirection.normalized, transform.forward);
		float speed = accel * speedFactor;
		transform.Translate(0, 0, Time.deltaTime * speed);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "waypoint")
		{
			if(currentWaypoint==1&&objWaypoint==2){
				PlayerPrefs.SetInt("STAGE",2);
				PlayerPrefs.Save();
				Application.LoadLevel(2);
			}
			if(currentWaypoint==3&&objWaypoint==4){
				PlayerPrefs.SetInt("STAGE",3);
				PlayerPrefs.Save();
				Application.LoadLevel(3);
			}
			if(currentWaypoint==5){

				Application.LoadLevel(4);
			}
			currentWaypoint++;
		}
	}
	void OnGUI()
	{
		if (GUI.Button(new Rect(70, 535, 100, 40), "", menuButton1))
		{
			objWaypoint = 0;
			PlayerPrefs.SetInt("STAGE",1);
			PlayerPrefs.Save();
			Application.LoadLevel(1);
		}
		if (GUI.Button(new Rect(250, 535, 100, 40), "", menuButton2))
		{
			objWaypoint = 2;
			//Application.LoadLevel(2);
		}
		if (GUI.Button(new Rect(420, 535, 100, 40), "", menuButton3))
		{
			objWaypoint = 4;
			//Application.LoadLevel(3);
		}
		if (GUI.Button(new Rect(580, 535, 100, 40), "", menuButton4))
		{
			PlayerPrefs.SetInt("STAGE",4);
			PlayerPrefs.Save();
			objWaypoint = 6;
			//Application.LoadLevel(4);
		}
	}
}
