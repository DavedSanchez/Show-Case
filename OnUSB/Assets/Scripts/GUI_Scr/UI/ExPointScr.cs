using UnityEngine;
using System.Collections;

public class ExPointScr : MonoBehaviour {
	private SpriteRenderer exPoint;
	private Animation ani;
	private BarScr bar;

	void Awake(){
		exPoint = GetComponent<SpriteRenderer> ();
		ani = GetComponent<Animation> ();
		bar = GameObject.Find ("Content").GetComponent<BarScr> ();
	}

	// Use this for initialization
	void Start () {
		exPoint.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (bar.Capacity >= .85f) {
			exPoint.enabled = true;
			ani.Play("resizingExPoint");
		}
		else
			exPoint.enabled = false;
	}
}
