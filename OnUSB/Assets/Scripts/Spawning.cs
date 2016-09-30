using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {
	private GameObject pauseMenue;
	private PauseMenueScr pauseScr;
	private GameObject soundMenue;

	public bool pause;

	public int fishCount;
	public float timeBeforeStart;
	public float timeBetweenFish;
	public float timeBetweenWaves;
	public Vector3 spawnValues;
	public GameObject Fish1;
	public GameObject Fish2;
	public GameObject Fish3;
	public GameObject Fish4;
	public GameObject Fish5;
	public GameObject Garbage1;
	public GameObject Garbage2;

	void Start() {
		StartCoroutine(this.spawnWaves());
		pause = false;
		pauseMenue = GameObject.Find ("PauseMenue");
		pauseScr = GameObject.Find ("PauseMenue").GetComponent<PauseMenueScr> ();
		pauseMenue.SetActive (false);
		soundMenue = GameObject.Find ("SoundMenue");
		pauseMenue.SetActive (false);

		timeBetweenFish = 2f;
	}

	void Update(){
		if (Input.GetKeyUp (KeyCode.Escape) && pauseScr.SoundActive == false)
			pause = !pause;

		if (pause) {
			Pause ();
		} else
			UnPause ();
	}
	
	IEnumerator spawnWaves() {
		yield return new WaitForSeconds(this.timeBeforeStart);
		while(true) {
			for(int i = 0; i < this.fishCount; i++) {
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y + 2, 0.0f);
				float angle = Random.Range(1, 360);
				Quaternion spawnRotation = Quaternion.AngleAxis(angle, Vector3.forward);
				if(Random.Range(0, 4) == 0) {
					if(Random.Range(0, 1) == 0) Instantiate(Garbage1, spawnPosition, spawnRotation);
					else Instantiate(Garbage2, spawnPosition, spawnRotation);
				}
				else {
					int l = Random.Range(1,100);
					if(l <= 40) Instantiate(this.Fish1, spawnPosition, spawnRotation);
					else if(l <= 70) Instantiate(this.Fish2, spawnPosition, spawnRotation);
					else if(l <= 85) Instantiate(this.Fish3, spawnPosition, spawnRotation);
					else if(l <= 95) Instantiate(this.Fish4, spawnPosition, spawnRotation);
					else Instantiate(this.Fish5, spawnPosition, spawnRotation);
				}
				yield return new WaitForSeconds(this.timeBetweenFish);
			}
			yield return new WaitForSeconds(this.timeBetweenWaves);
		}
	}

	void Pause(){
		Time.timeScale = 0;
		pauseMenue.SetActive (true);
	}

	void UnPause(){
		pauseMenue.SetActive (false);
		Time.timeScale = 1;
	}
}