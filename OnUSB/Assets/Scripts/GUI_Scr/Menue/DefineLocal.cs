using UnityEngine;
using System.Collections;

public class DefineLocal : MonoBehaviour {
	public string loc;

	// Use this for initialization
	void Start () {
		gameObject.name = loc;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
