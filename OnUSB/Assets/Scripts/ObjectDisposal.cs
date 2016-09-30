using UnityEngine;
using System.Collections;

public class ObjectDisposal : MonoBehaviour {

	private Net netScript;

	void Start() {
		netScript = GameObject.Find ("Player").GetComponent<Net> ();
	}

	void OnCollisionEnter2D(Collision2D col) {
		Destroy(col.gameObject);
		if(col.gameObject.tag == "Fish") {
			netScript.spawnScript.timeBetweenFish = 1;
			netScript.multiplier = 1;
		}
	}
	
}