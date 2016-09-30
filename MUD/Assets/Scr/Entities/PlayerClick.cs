using UnityEngine;
using System.Collections;

/*
 * Left click to ONLY move
 * Right click to move and initiate in combat iff enemy was clickd
 * if roaming and intercepting and enemy, stop moving to prevent trippy-buggy collision
 * 
 */

public class PlayerClick : MonoBehaviour {
	NavMeshAgent nav;
	public GameObject cam;

	bool isActive;
	RaycastHit temp;

	/*player Stuff*/
	Stats player;
	Attack atk;
	bool inRange;
	int collisionRange;

	// Use this for initialization
	void Start() {
		NavMesh.CalculateTriangulation ();
		nav = GetComponent<NavMeshAgent> ();
		Debug.Log (nav.isOnNavMesh);
		isActive = false;

		player = GetComponent<Stats>();
		atk = GetComponent<Attack>();

		inRange = false;
		collisionRange = 2;
	}
	
	// Update is called once per frame
	void Update() {			

		if (Input.GetKeyDown (KeyCode.P))
			cam.GetComponent<Camera> ().orthographic = false;

		if (Input.GetKeyDown (KeyCode.O))
			cam.GetComponent<Camera> ().orthographic = true;

		//for click movements
		RaycastHit hit;
		//RaycastHit collisionHit;
		//determins where player will move to
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
		//players range detections
		Ray playerRay = new Ray (transform.position, transform.forward * player.Range);
		//Ray to detect collision with enemys
		//Ray collisionRay = new Ray(transform.position, transform.forward * collisionRange);

		//logic to see if player is moving or attacking
		if (Physics.Raycast(ray, out hit, 100)) {
			//if player left clicks, then only move
			if (Input.GetMouseButtonDown (0))
				nav.SetDestination (hit.point);

			//if player right clicks, do either attack or move to then attack
			if (Input.GetMouseButtonDown(1)) {
				//if player is close enough for auto attcks, then do them
				if (hit.collider.tag == "Enemy" && getDistance (hit.collider) <= player.Range) {
					inRange = true;
					isActive = true;
					transform.LookAt (hit.transform);
				//if player is too far, move towards enemy till within range
				}else if (hit.collider.tag == "Enemy"  && getDistance (hit.collider) > player.Range) {
					inRange = false;
					isActive = true;
					nav.SetDestination (hit.point);
					transform.LookAt (hit.transform);
				} else {
					//logic for moving with right click
					isActive = false;
					nav.SetDestination (hit.point);
					transform.LookAt (hit.transform);
				}
			}
		}

		//check if the player has moved enough to get in range for autos
		if (Physics.Raycast (playerRay, out hit, player.Range)){
			if (hit.collider.tag == "Enemy" && isActive == true) {
				nav.SetDestination (transform.position);
				inRange = true;
				Debug.Log ("In Range.");
			} else if (hit.collider.tag != "Enemy") {
				inRange = false;
				isActive = false;
			}
		}

		Debug.DrawRay (transform.position, transform.forward * collisionRange, Color.red);

		/*if (Physics.Raycast(collisionRay, out collisionHit, collisionRange) && isActive == false) {
			if (collisionHit.collider.tag == "Enemy") {					
				//inRange = true;
				Debug.Log("Collided with enemy");
				nav.SetDestination (transform.position);
			}
		}*/

		//do basic (auto) attack damage
		if (inRange && isActive)
			atk.basicAttack ();
	}

	//simple function go get the distance from player to desired enemy
	private float getDistance(Collider col){
		return Mathf.Sqrt (Mathf.Pow (col.transform.position.x - player.transform.position.x, 2) + Mathf.Pow (col.transform.position.z - player.transform.position.z, 2));
	}
}