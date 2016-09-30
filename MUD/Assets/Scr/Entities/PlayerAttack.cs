using UnityEngine;
using System.Collections;

public class PlayerAttack : Attack {

	Stats playerStats;

	// Use this for initialization
	void Start () {
		playerStats = GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override int basicAttack(){
		Debug.Log ("Basic Attack");
		return playerStats.Damage;
	}

	public override int useAbility(){
		Debug.Log ("using abilities");
		return 0;
	}
}
