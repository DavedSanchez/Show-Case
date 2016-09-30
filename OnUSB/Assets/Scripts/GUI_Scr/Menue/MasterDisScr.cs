using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MasterDisScr : MonoBehaviour {
	private Text masterDis;
	private Slider masterSlider;

	void Awake(){
		masterDis = GetComponent<Text> ();
		masterSlider = GameObject.Find ("Master_Slider").GetComponent<Slider>();
	}

	// Use this for initialization
	void Start () {
		masterDis.text = masterSlider.value.ToString ();
	}

	public void UpdateMasterDis(){
		masterDis.text = masterSlider.value.ToString();
	}
}
