using UnityEngine;
using System.Collections;

/*
 * Allows the player to climb stars. That's it...
*/

public class StairTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Player")
			col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			col.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}
}
