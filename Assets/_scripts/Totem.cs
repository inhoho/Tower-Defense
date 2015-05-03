using UnityEngine;
using System.Collections;

public class Totem : MonoBehaviour {
	
	private string ttag = "enemyAim";
	private Transform target;
	private Transform closestEnemy = null;
	private float dist;
	private GameObject[] taggedEnemys;
	private float closestDistSqr = Mathf.Infinity;
	private Vector3 objectPos;
	private float startTime;
	private float shootTimeLeft;
	private float shootTimeSeconds = 1.0f;
	
	
	public GameObject projPrefab;
	public Vector3 targetDir;

	
	
	void Start () 
	{
		InvokeRepeating("getClosestEnemy", 0, 1.0f);
	}
	
	
	void Update () 
	{
		if (target != null)
		{
			targetDir = target.position - transform.position;
			transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
			shootTimeLeft = Time.time - startTime;
			
			if (shootTimeLeft >= shootTimeSeconds)
			{
				Fire();
				print("Fire");
				startTime = Time.time;
				shootTimeLeft = 0;
			}
			Debug.DrawLine(transform.position, target.position, Color.red);
		}
	}
	
	void Fire()
	{
		GameObject instantiateProjectile = Instantiate(projPrefab, transform.position, transform.rotation) as GameObject;
		
		
	}
	
	void getClosestEnemy()
	{
		taggedEnemys = GameObject.FindGameObjectsWithTag(ttag);
		
		foreach(GameObject taggedEnemy in taggedEnemys)
		{
			objectPos = taggedEnemy.transform.position;
			dist = (objectPos - transform.position).sqrMagnitude;
			
			if (dist < 2.0f)
			{
				if (dist < closestDistSqr)
				{
					closestDistSqr = dist;
					closestEnemy = taggedEnemy.transform;
				}
			}
		}
		target = closestEnemy;
			
	}	
}
