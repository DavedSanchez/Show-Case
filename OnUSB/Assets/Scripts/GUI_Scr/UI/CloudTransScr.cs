using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;

public class CloudTransScr : MonoBehaviour {
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float killTimer = 8f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, killTimer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * speed);	

	}
}
