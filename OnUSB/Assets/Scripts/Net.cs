using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

class Net : MonoBehaviour {
	private Rigidbody2D rb;
	private List<FishingObject> contents;
	public float currentCapacity;
	public float maxCapacity;
	public int score;
	public int multiplier;
	public float speed;
	public float xMin;
	public float xMax;
	public Spawning spawnScript;

	//moded vars;
	private int withold;

	void Start() {
		this.contents = new List<FishingObject>();
		rb = GetComponent<Rigidbody2D>();
		maxCapacity = 100;
		withold = 0;
		score = 0;
		spawnScript = GameObject.Find ("GameController").GetComponent<Spawning> ();
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.E)) this.empty();

		if(multiplier == 1)
			spawnScript.timeBetweenFish = 2f;
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal, 0.0f);
		rb.velocity = movement * speed;
		rb.position = new Vector2(Mathf.Clamp(rb.position.x, this.xMin, this.xMax), 0.0f);
	}

	void OnCollisionEnter2D(Collision2D col) {
		FishingObject obj = col.gameObject.GetComponent<FishingObject>();

		if ((currentCapacity + obj.size) > maxCapacity) {
			obj.rb.AddForce ((Vector2.right * 100) + (Vector2.up * 1000));
		}
		else if(col.gameObject.tag == "Fish") {
			Fish f = col.gameObject.GetComponent<Fish>();
			Destroy(col.gameObject);
			if(this.currentCapacity + f.size <= this.maxCapacity) {
				// Debug
				Debug.Log("ADDED " + f.size + " TO THE NET");
				this.currentCapacity += f.size;
				/*moded keep value*/
				withold += f.value;
				this.contents.Add(f);
				this.multiplier++;
				spawnScript.timeBetweenFish -= 0.05f;
				// Debug
				Debug.Log("MULTIPLIER INCREASED TO " + this.multiplier);
			}
			else {
				// Debug
				Debug.Log("NET TOO FULL TO ADD " + f.size);
				this.multiplier = 1;
				// Debug
				Debug.Log("MULTIPLIER BROKEN: NET FULL YET ATTEMPTED TO CATCH OBJECT");
			}
		}
		else if(col.gameObject.tag == "Garbage") {
			Garbage g = col.gameObject.GetComponent<Garbage>();
			Destroy(col.gameObject);
			if(this.currentCapacity + g.size <= this.maxCapacity) {
				// Debug
				Debug.Log("ADDED " + g.size + " TO THE NET");
				this.currentCapacity += g.size;
				this.contents.Add(g);
				this.multiplier = 1;
				spawnScript.timeBetweenFish = 1;
				// Debug
				Debug.Log("MULTIPLIER BROKEN: CAUGHT GARBAGE");
			}
			else {
				// Debug
				Debug.Log("NET TOO FULL TO ADD " + g.size);
				this.multiplier = 1;
				// Debug
				Debug.Log("MULTIPLIER BROKEN: NET FULL YET ATTEMPTED TO CATCH OBJECT");
			}
		}
}
		
	public bool isEmpty() {
		if(this.currentCapacity == 0) return true;
		else return false;
	}

	public List<FishingObject> copyContents() {
		List<FishingObject> contents = new List<FishingObject>();
		for(int i = 0; i < this.contents.Count; i++) contents.Add(this.contents[i]);
		return contents;
	}
	
	public bool empty() {
		if(!this.isEmpty()) {
			// Debug
			Debug.Log("EMPTIED NET");
			score += (withold * multiplier);
			withold = 0;
			this.currentCapacity = 0;
			this.contents.Clear();
			return true;
		}
		else {
			// Debug
			Debug.Log("NET ALREADY EMPTY");
			return false;
		}
	}
	
	public void emptyAndAddToScore() {
		List<FishingObject> contents = this.copyContents();
		if (this.empty ())
			foreach (FishingObject obj in contents) {
				if (obj is Fish) {
					Fish f = (Fish)obj;
					score += f.value * this.multiplier;
				}
			}
	}
	
}