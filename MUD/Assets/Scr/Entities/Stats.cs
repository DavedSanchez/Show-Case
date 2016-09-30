using UnityEngine;
using System.Collections;

/*
 * Very basic stats scr
 * will (or can) be used for both players an enemies
 */

public class Stats : MonoBehaviour {
	private string _name;
	private int _str, _dex, _vit, _basicAttackRange, _basicAttackDamage, _attacksPerSecond;
	private float _health, _mana;

	private bool _pulledAggro;

	// Use this for initialization
	void Start () {
		_str = _dex = _vit = _basicAttackRange = 10;
		_basicAttackDamage = 10;
		name = "Player1";

		_health = _mana = 100f;

		_pulledAggro = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K))
			_health = 0;

		if (Input.GetKeyDown (KeyCode.H))
			_health = 100;

		if (_health <= 0) {
			gameObject.GetComponent<MeshRenderer> ().material.color = new Color (1f, 1f, 1f, 0.5f);
			gameObject.GetComponent<PlayerClick> ().enabled = false;
		}

		Debug.Log (_health);
	}

	public int Strength{
		get{ return _str;}
	}

	public int Dexterity{
		get{ return _dex;}
	}

	public int Vitality{
		get{ return _vit;}
	}

	public int Range{
		get{ return _basicAttackRange;}
	}

	public int Damage{
		get{ return _basicAttackDamage;}
	}
	
	public float Health{
		get{ return _health;}
		set{ _health = value;}
	}

	public float Mana{
		get{ return _mana;}
		set{ _mana = value;}
	}

	public bool PulledAggro{
		get{ return _pulledAggro;}
		set{ _pulledAggro = value;}
	}
}
