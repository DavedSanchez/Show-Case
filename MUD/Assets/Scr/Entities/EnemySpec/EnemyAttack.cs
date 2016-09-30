using UnityEngine;
using System.Collections;

public class EnemyAttack : Attack {
	public int damage;
	public int range;
	public int abilityPower;
	public float attacksPerSecond;

	private float delay;
	float nextAttack;

	bool inMeleeRange;

	Collider player;

	// Use this for initialization
	void Start () {
		inMeleeRange = false;
		delay = 1f / attacksPerSecond;
	}
	
	// Update is called once per frame
	void Update () {
		if (inMeleeRange && Time.time > nextAttack) {
			nextAttack = Time.time + delay;
			player.GetComponent<Stats> ().Health -= basicAttack ();
		}
	}

	public override int basicAttack() {
		Debug.Log ("Enemy basic attack");
		return damage;
	}

	public override int useAbility(){
		Debug.Log ("Enemy Ability");
		return abilityPower;
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player")
			GetComponent<NavMeshAgent> ().SetDestination (transform.position);
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Player"){
			inMeleeRange = true;//col.GetComponent<Stats> ().Health -= damage;
			player = col;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			inMeleeRange = false;
			
		}
	}
}
