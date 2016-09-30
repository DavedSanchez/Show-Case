using UnityEngine;
using System.Collections;

/*
 * Logic to make walls/environmental objs trans opaque
 *
*/

public class HitDetection : MonoBehaviour {
	[SerializeField]
	private bool _isHit;

	// Use this for initialization
	void Awake(){
		_isHit = false;
	}
	
	// Update is called once per frame
	void Update () {
		//make wall transparent
		if (_isHit == true)
			gameObject.GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 0.5f);
		else //make opaque
			gameObject.GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 1f);
	}
		
	//'called' func from follow cam scr to set _isHit to true
	private void HitByRay(){
		_isHit = true;
	}

	//'called' func from follow cam scr to set _isHit to false
	private void NotHitByRay(){
		_isHit = false;
	}

	public bool IsHit{
		get{ return _isHit;}
		set{ _isHit = value;}
	}
}
