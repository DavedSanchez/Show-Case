using UnityEngine;
using System.Collections;

/*
 * Basic attack scr for player and enemies
 */

public abstract class Attack : MonoBehaviour {
	abstract public int basicAttack ();
	abstract public int useAbility ();
}
