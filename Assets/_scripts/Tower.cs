using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	
	private string targetTag = "enemyAim";
	private Transform target;
	private float dist;
	private Vector3 objectPos;
	private float startTime;
	private float shootTimeLeft;
	private float shootTimeSeconds = .01f;
	private float initialSpeed = 10.0f;
	private Transform closestEnemy = null;
	
	
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
			shootTimeLeft = Time.time - startTime;
			
			if (shootTimeLeft >= shootTimeSeconds)
			{
				Fire();
				startTime = Time.time;
				shootTimeLeft = 0;
			}
			Debug.DrawLine(transform.position, target.position, Color.yellow);
		}
	}
	
	
	void Fire()
	{
		GameObject instantiateProjectile = Instantiate(projPrefab, transform.position, transform.rotation) as GameObject;
		
		instantiateProjectile.rigidbody.velocity = transform.TransformDirection(targetDir*initialSpeed);
	}
	
	void getClosestEnemy()
	{
		
		GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag(targetTag);
		float closestDistSqr = Mathf.Infinity;
		closestEnemy = null;
		
		foreach(GameObject taggedEnemy in taggedEnemys)
		{
			objectPos = taggedEnemy.transform.position;
			dist = (objectPos - transform.position).sqrMagnitude;
			
			if (dist < 3.0f)
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
