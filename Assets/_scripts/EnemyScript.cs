using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	
	public Transform[] waypoints = new Transform[9];
	public Transform activeWaypoint;
	public int currentWaypoint = 0;
	public GameObject gameObj;
	private Game gameScript;
	public GameObject deathParticle;

	private int state = 0;
	private Transform myPos;
	private float accel = 0.6f;
	private float rotationDamping = 6.0f;
	private bool dead = false;

	private HealthBarScript healthBarScript;
	
	private GameObject wp10bj;
	private GameObject wp20bj;
	private GameObject wp30bj;
	private GameObject wp40bj;
	private GameObject wp50bj;
	private GameObject wp60bj;
	private GameObject wp70bj;
	private GameObject wp80bj;
	private GameObject wp90bj;
	
	void Start () 
	{
		gameObj = GameObject.Find("game");
		gameScript = (Game)gameObj.transform.gameObject.GetComponent("Game");
	
		healthBarScript = (HealthBarScript)GetComponent("HealthBarScript");
		currentWaypoint = 0;
		state = 0;
		myPos = transform;
		
		wp10bj = GameObject.Find("waypoint_01");
		wp20bj = GameObject.Find("waypoint_02");
		wp30bj = GameObject.Find("waypoint_03");
		wp40bj = GameObject.Find("waypoint_04");
		wp50bj = GameObject.Find("waypoint_05");
		wp60bj = GameObject.Find("waypoint_06");
		wp70bj = GameObject.Find("waypoint_07");
		wp80bj = GameObject.Find("waypoint_08");
		wp90bj = GameObject.Find("waypoint_09");
		
		waypoints[0] = wp10bj.transform;
		waypoints[1] = wp20bj.transform;
		waypoints[2] = wp30bj.transform;
		waypoints[3] = wp40bj.transform;
		waypoints[4] = wp50bj.transform;
		waypoints[5] = wp60bj.transform;
		waypoints[6] = wp70bj.transform;
		waypoints[7] = wp80bj.transform;
		waypoints[8] = wp90bj.transform;
		
		
	}
	
	void Update () 
	{
		if (state == 0)
		{
			if (currentWaypoint != 9)
			{
				walk();
				activeWaypoint = waypoints[currentWaypoint];
			}
			else
			{
				
				if (gameScript.baseHealth != 0)
				{
					gameScript.baseHealth -= 10;
					gameScript.enemiesLeft -= 1;
					
				}
				enemySacrifice();
				Destroy(this.gameObject);
				Destroy(healthBarScript.healthBarObj);

			}
		}
		
		HealthBarScript script = (HealthBarScript)GetComponent("HealthBarScript");
		if (dead != true)
		{
			if (script.currentHealth <= 0)
			{
				gameScript.playersWood += 20;
				gameScript.playerScore =gameScript.stage*18+1000+gameScript.playerScore;
				gameScript.enemiesLeft -= 1;
				gameScript.money+=1000;

				StartCoroutine(playDeath());
				dead = true;
			}
		}
			
		//Debug.DrawLine(myPos.position, activeWaypoint.position, Color.red);
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
			currentWaypoint++;
			
		}
	}
	
	
	IEnumerator playDeath()
	{
		
		state = 1;
		Animation tribeAnimation = (Animation)GetComponentInChildren<Animation>();
		tribeAnimation.animation.Play("death");
		audio.Play();
		
		yield return new WaitForSeconds(1);
		
		Destroy(this.gameObject);
		Destroy(healthBarScript.healthBarObj);
		Destroy(healthBarScript);
	}
	
	void enemySacrifice() 
	{
		GameObject deathSmoke = (GameObject)Instantiate(deathParticle,
		                                                this.transform.position,
		                                                this.transform.rotation);
		Destroy(this.gameObject);
	}
		
		
}
