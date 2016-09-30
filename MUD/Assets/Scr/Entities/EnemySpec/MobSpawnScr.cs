using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * maxEnemyCount will be used later globally for a dungeon
 * Spawns a number of units [1, 10] randomly
 * All units are chosen randomly from the unit pool (more units will come later)
 * If all units belonging to a mob spawn point are terminated, then the mod spawn will be removed
 * When an enemy enters into the proximity of a mob spawn, all units are alerted
*/

public class MobSpawnScr : MonoBehaviour {
	public GameObject[] enemyPrefabs; 
	public int radius;

	int maxEnemyCount, numberOfEnemies;

	void Awake(){
		//generate max possible num units
		maxEnemyCount = Random.Range (3, 10);
		//generate actual num units
		numberOfEnemies = Random.Range (3, maxEnemyCount);

		//spawn each unit
		//declare that each spawned unit as a child to help with removing the mob spawn later
		for (int i = 0; i < numberOfEnemies; i++) {
			GameObject spawn = Instantiate (enemyPrefabs [Random.Range (0, enemyPrefabs.Length)], transform.position + (Random.insideUnitSphere * radius), Quaternion.Euler(0, Random.Range(1, 360), 0)) as GameObject;
			spawn.transform.parent = gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if all units are terminated, remove the spawn point
		if (transform.childCount <= 0)
			Destroy (gameObject);
	}

	//if a player enters in the proximity of the spawn point, alert all units
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			for (int i = 0; i < transform.childCount; i++)
				transform.GetChild (i).GetComponent<EnemyAggro> ().Aggroed (col);
		}
	}
}
