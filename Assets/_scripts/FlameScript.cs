using UnityEngine;
using System.Collections;

public class FlameScript : MonoBehaviour 
{
    
	
	void OnTriggerEnter(Collider enemy) 
	{
		if (enemy.tag == "enemyAim") 
		{
			print("hit");
			HealthBarScript script = (HealthBarScript)enemy.transform.root.gameObject.GetComponent("HealthBarScript");
			if (script.currentHealth != 0)
			{
				script.currentHealth -= 15;
			}
		}
	}
}
