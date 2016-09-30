using UnityEngine;
using System.Collections;

/*
 * Scr only to 'sense' if player is in aggro range
 * This is to prevent raycast collision and enable player to click right next to enemies
*/

public class AggroScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//player enter aggro area
	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy") {
			//call the function in the parent class and pass the collider arg
			//if(transform.parent.GetComponent<Stats>().PulledAggro == false)
				col.transform.SendMessage ("Aggroed", transform.parent.GetComponent<Collider>());
		}
	}
}
