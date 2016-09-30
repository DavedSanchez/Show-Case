using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This script is to detect if the player is behind a piece of environment
 * if so, then the piece will be made transparent
 * when the player is no longer behind it, it returns to opaque
 * 
 */

public class CameraFollow : MonoBehaviour {
	//player
	public GameObject player;
	//camera off set so that shit stays tight with player
	public Vector3 offset;

	//camera's range components
	public float distance;
	RaycastHit hit;
	Ray ray;

	//list to hold enviroment pieces in case beind consequtive envi pcs
	[SerializeField]
	List<Collider> colList;

	// Use this for initialization
	void Start () {
		//player = GameObject.Find("Player");
		offset = transform.transform.position - player.transform.position;
		colList = new List<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		ray = new Ray (transform.position, transform.forward * distance);
		Debug.DrawRay (transform.position, transform.forward * distance, Color.red);

		if (Physics.Raycast (ray, out hit, distance)) {
			//if player is behind a wall and the wall is opaque, make is transparent then add to colList
			if (hit.collider.tag == "Wall" && hit.collider.GetComponent<HitDetection> ().IsHit == false) {
				hit.collider.GetComponent<HitDetection> ().IsHit = true;//hit.transform.SendMessage ("HitByRay");
				colList.Add (hit.collider);
			//if player is not beind enviro pcs, make the transparent pc opaque
			} else if(hit.collider.tag != "Wall"){
				//makes sure the list isn't empty
				if (colList.Count > 0) {
					//hit.collider.GetComponent<HitDetection> ().IsHit = false;
					colList[0].GetComponent<HitDetection>().IsHit = false;//colList [0].SendMessage ("NotHitByRay");	
					pop (colList);
				}
			//if player goes from one enviro pc to another, make the "old" enviro pc opaque
			//it is assumed that a player cant be "behind" more than 2 pcs 
			}else if(hit.collider.tag == "Wall" && colList.Count == 2){
				colList [0].SendMessage ("NotHitByRay");
				pop (colList);
			}
		}

		//follow the player
		transform.position = player.transform.position + offset;
	}


	//function to alter list by removing the first item in list (acts now as a queue)
	private static List<Collider> pop(List<Collider> l){
		l.RemoveAt (0);
		return l;
	}
}
