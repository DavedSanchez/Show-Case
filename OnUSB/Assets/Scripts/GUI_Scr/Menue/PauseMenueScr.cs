using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenueScr : MonoBehaviour {
	private GameObject pauseMenue;
	private GameObject soundMenue;
	private Text lvlDis;

	private bool _isActive = false;

	public string local;

	// Use this for initialization
	void Start () {
		pauseMenue = GameObject.Find ("PauseMenue");
		soundMenue = GameObject.Find ("SoundMenue");
		lvlDis = GameObject.Find ("Lvl_Dis").GetComponent<Text> ();

		soundMenue.SetActive (false);
		SoundActive = false;
	
		lvlDis.text = "Local: " + local + "\tLevel: Beta";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			SoundActive = !SoundActive;
			soundMenue.SetActive (false);
		}
	}

	public void Resume(){
		GameObject.Find ("GameController").GetComponent<Spawning> ().pause = false;
	}

	public void Quit(){
		Application.Quit ();
		Debug.Log ("Quitting Application");
	}

	public void Sound(){
		soundMenue.SetActive (true);
		SoundActive = true;
	}

	public void Return(){
		soundMenue.SetActive (false);
		SoundActive = false;
	}

	public bool SoundActive{
		get{ 
			return _isActive;
		}

		set{ 
			_isActive = value;
		}
	}
}
