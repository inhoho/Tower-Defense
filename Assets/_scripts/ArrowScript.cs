using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour 
{
    public GameObject particleSystem;
	public GameObject gameObj;
	private Game gameScript;
	void Start()
	{
				gameObj = GameObject.Find ("game");
				gameScript = (Game)gameObj.transform.gameObject.GetComponent ("Game");
	}
	void Update()
	{

	}
	IEnumerator OnTriggerEnter(Collider enemy) 
	{
		if (enemy.tag == "enemyAim") 
		{
			GameObject ps = (GameObject)Instantiate(particleSystem, this.transform.position, this.transform.rotation);
			HealthBarScript script = (HealthBarScript)enemy.transform.root.gameObject.GetComponent("HealthBarScript");
			if (script.currentHealth != 0)
			{

				script.currentHealth -= .15f;
			}
			
			Destroy(this.gameObject);
		
		} 
		else
		{
			yield return new WaitForSeconds(2);
			Destroy(this.gameObject);	
		
		}
	}
}
