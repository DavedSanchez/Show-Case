using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameAudio : MonoBehaviour {
	private AudioSource music;

	private Slider masterSlider;
	private Slider musicSlider;

	void Awake(){
		masterSlider = GameObject.Find ("Master_Slider").GetComponent<Slider> ();
		musicSlider = GameObject.Find ("Music_Slider").GetComponent<Slider> ();

		music = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AdjustMaster(){
		AudioListener.volume = (masterSlider.value / 10f);
	}

	public void AdjustMusic(){
		music.volume = (musicSlider.value / 10f);
	}
}
