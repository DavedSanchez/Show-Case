using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScr : MonoBehaviour {
	private Text scoreText;
	private int score;

	private GameObject player;

	void Awake(){
		scoreText = GetComponent<Text> ();
		player = GameObject.Find ("Player");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		score = player.GetComponent<Net>().score;
		Score = score;
		//Debug.Log (player.GetComponent<Net>().empty().ToString());
		scoreText.text = Score.ToString();

	}

	public int Score{
		get{ 
			return score;
		}

		set{ 
			score = value;
		}
	}
}
