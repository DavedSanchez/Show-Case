using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplierDisplay : MonoBehaviour {
	private Text muDis;
	private int disAmt;

	private GameObject player;

	void Awake(){
		muDis = GetComponent<Text> ();
		player = GameObject.Find ("Player");
	}
		
	// Update is called once per frame
	void Update () {
		disAmt = player.GetComponent<Net>().multiplier;
		Multiplier = disAmt;
		muDis.text = "x" + Multiplier.ToString();
	}

	public int Multiplier{
		get{ 
			return disAmt;
		}

		set{ 
			disAmt = value;
		}
	}
}
