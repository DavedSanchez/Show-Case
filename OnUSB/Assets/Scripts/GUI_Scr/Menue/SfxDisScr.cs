using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SfxDisScr : MonoBehaviour {
	private Text sfxDis;
	private Slider sfxSlider;

	void Awake(){
		sfxDis = GetComponent<Text> ();
		sfxSlider = GameObject.Find ("SFX_Slider").GetComponent<Slider>();
	}

	// Use this for initialization
	void Start () {
		sfxDis.text = sfxSlider.value.ToString ();
	}

	public void UpdateSfxDis(){
		sfxDis.text = sfxSlider.value.ToString();
	}
}
