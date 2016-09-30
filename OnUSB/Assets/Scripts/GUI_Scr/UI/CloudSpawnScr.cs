using UnityEngine;
using System.Collections;

public class CloudSpawnScr : MonoBehaviour {
	private GameObject cloud;
	public Transform spawn;

	public GameObject[] cloudCol;
	private int cloudIndex;
	//private float speed = 5f;

	private GameObject cloudInstance;

	private float spawnRate = 2f;
	private float nextSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			cloudIndex = Random.Range (0, cloudCol.Length);
			cloud = cloudCol [cloudIndex];
			spawnRate = Random.Range (2, 6);
			nextSpawn = Time.time + spawnRate;
			cloudInstance = Instantiate (cloud,  new Vector3(spawn.position.x, (float)Random.Range(spawn.position.y - 1, spawn.position.y + 2), spawn.position.z),spawn.rotation) as GameObject;
		}
	}
}
