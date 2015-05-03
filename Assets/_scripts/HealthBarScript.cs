using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour 
{
	
	public GameObject healthBarPrefab;
	public GameObject healthBarObj;

	public float currentHealth;
	public float maxHealth;
	public float healthBarWidth;
	
	public Game Gamescript;
	public GameObject GameObj;

	// Use this for initialization
	void Start () 
	{
		GameObj = GameObject.Find("game");
		Gamescript = (Game)GameObj.transform.gameObject.GetComponent("Game");
		maxHealth += Gamescript.stage * 18;// 스테이지별 체력의 증가.
		currentHealth = maxHealth;
		healthBarObj = Instantiate(healthBarPrefab, transform.position, transform.rotation) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
				                                              
		healthBarObj.transform.position = Camera.main.WorldToViewportPoint(transform.position);
		healthBarObj.transform.position = new Vector3(healthBarObj.transform.position.x - 0.025f,
		                                              healthBarObj.transform.position.y + 0.08f,
		                                              healthBarObj.transform.position.z);
		
		
		float healthPercent = currentHealth/maxHealth;
		
		if(healthPercent < 0) 
		{
			healthPercent = 0;
		}
		
		healthBarWidth = healthPercent * 20;
		healthBarObj.guiTexture.pixelInset = new Rect(10, 10, healthBarWidth, 5);
	
	}
}
