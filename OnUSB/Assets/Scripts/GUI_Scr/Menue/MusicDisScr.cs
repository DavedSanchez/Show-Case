using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicDisScr : MonoBehaviour {
	private Text musicDis;
	private Slider musicSlider;

	void Awake(){
		musicDis = GetComponent<Text> ();
		musicSlider = GameObject.Find ("Music_Slider").GetComponent<Slider>();
	}

	// Use this for initialization
	void Start () {
		musicDis.text = musicSlider.value.ToString ();
	}

	public void UpdateMusicDis(){
		musicDis.text = musicSlider.value.ToString();
	}
}
