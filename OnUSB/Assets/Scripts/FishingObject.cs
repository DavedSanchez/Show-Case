using UnityEngine;
	
abstract class FishingObject : MonoBehaviour {
	
	public Rigidbody2D rb;
	public int size;
	public float tumble;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.angularVelocity = Random.Range(-20,20) * tumble;
	}
	
}