using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Scr to allow enemies to set aggro on players
 * Players entering in the trigger collider till be targeted
 * All players entering trigger will be stored in be plaved in a list
 * iff the 'first' player dies, the enemy aggros the 'next' player
 * Enemies will shift aggro accordingly to threat mechanic
*/

public class EnemyAggro : MonoBehaviour {
	NavMeshAgent nav;

	bool _isAggroed;

	[SerializeField]
	List<Collider> playerList;

	Vector3 initLocation;


	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		_isAggroed = false;

		playerList = new List<Collider> ();
	
		initLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//follow enemies when _isChasing is true
		//also only chase if playerList has somthing in it
		if (_isAggroed && playerList.Count > 0) {
			//if first player who too aggro is still alive, follow them still
			if (playerList [0].GetComponent<Stats> ().Health > 0) {
				nav.SetDestination (playerList [0].transform.position);
			} else {     
				//if that particular player is dead, deque and go for the next player
				deque (playerList);

				//look for the closet player, if all players are dead, drop aggro
				if (playerList.Count > 0)
					nav.SetDestination (playerList [findClosestPlayer (playerList)].transform.position);
				else
					_isAggroed = false;
			}

			//TODO: make a threat mechanic logic here
		} else
			nav.SetDestination (initLocation);		//if a player in the list is dead and no other players pulled aggro, units go back to initial loc
	}

	//Detects of enemy is withing aggro range
	public void Aggroed(Collider col){
		_isAggroed = true;

		//prevents the same player being enqueued more tha once
		if (playerNotEnque (playerList, col)) {
			col.GetComponent<Stats> ().PulledAggro = true;
			playerList.Add (col);
		}
	}

	//remove enemy in front of 'queue'
	private static List<Collider> deque(List<Collider> l){
		if (l.Count > 0)
			l.RemoveAt (0);

		return l;
	}

	//gets the distance from unit to player
	private float getDistance(Collider player){
		return Mathf.Sqrt (Mathf.Pow (transform.position.x - player.transform.position.x, 2) + Mathf.Pow (transform.position.x - player.transform.position.y, 2) + Mathf.Pow(transform.position.z - player.transform.position.z, 2));
	}

	//find the closet player within playerList
	private int findClosestPlayer(List<Collider> playerList){
		int index = 0;
		float closestplayer = getDistance(playerList[0]);

		for (int i = 1; i < playerList.Count; i++) {
			float dist = getDistance(playerList[i]);
			if (closestplayer >= dist) {
				closestplayer = dist;
				index = i;
			}
		}

		return index;
	}

	//go though playerList
	//if player is already in the list, dont add again
	//else add the player
	private bool playerNotEnque(List<Collider> l, Collider player){
		foreach (Collider col in l) {
			if (col.name == player.name)
				return false;
		}

		return true;
	}

	public bool IsAggroed{
		set{ _isAggroed = value;}
	}
}
