using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScr : MonoBehaviour {
	[SerializeField]
	private Image content;
	private Image jump;
	private Image star;

	public GameObject player;
	private float current;
	private float max;
	private float lerpSpeed;
	//private float jumpSpeed;

	private float rotSpeed;

	void Awake(){
		content = GetComponent<Image>();
		jump = (Image)GameObject.Find ("Jump").GetComponent<Image> ();

		star = (Image)GameObject.Find ("Star").GetComponent<Image>();
		star.enabled = false;

		content.fillAmount = 0f;
		lerpSpeed = 2f;
		//jumpSpeed = 10f;
		rotSpeed = 50f;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		current = player.GetComponent<Net>().currentCapacity;
		max = player.GetComponent<Net>().maxCapacity;
		HandleBar();

		if (MapFill (current, max) >= .75f) {
			star.enabled = true;
			star.transform.Rotate (0, 0, -Time.deltaTime * rotSpeed);
		}
		else
			star.enabled = false;
	}

	private void HandleBar(){
		content.fillAmount = Mathf.Lerp(content.fillAmount, MapFill(current, max), Time.deltaTime * lerpSpeed);
		jump.fillAmount = MapFill (current, max); //Mathf.Lerp(content.fillAmount, MapFill(current, max), Time.deltaTime * jumpSpeed);
	}

	private float MapFill (float value, float inMax){
		return value / inMax;
	}

	public float Capacity{
		get{ 
			return content.fillAmount;
		}
	}
}
