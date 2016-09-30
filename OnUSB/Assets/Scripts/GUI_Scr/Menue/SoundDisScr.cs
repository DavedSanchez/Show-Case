using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundDisScr : MonoBehaviour {
	private Text masterDis;
	private Text musicDis;
	private Text sfxDis;

	private Slider masterSlider;
	private Slider musicSlider;
	private Slider sfxSlider;

	// Use this for initialization
	void Awake () {
		masterDis = GameObject.Find ("Master_Txt").GetComponent<Text> ();
		musicDis = GameObject.Find ("Music_Txt").GetComponent<Text> ();

		masterSlider = GameObject.Find ("Master_Slider").GetComponent<Slider>();
		musicSlider = GameObject.Find ("Music_Slider").GetComponent<Slider>();
		sfxSlider = GameObject.Find ("SFX_Slider").GetComponent<Slider>();
	}

	void Start(){
		masterDis.text = masterSlider.value.ToString ();
		musicDis.text = musicSlider.value.ToString ();
		sfxDis.text = sfxSlider.value.ToString ();

		masterSlider.onValueChanged.AddListener (delegate {checkMaster ();});
		musicSlider.onValueChanged.AddListener (delegate {checkMusic ();});
		sfxSlider.onValueChanged.AddListener (delegate {checkSFX ();});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void checkMaster(){
		masterDis.text = masterSlider.value.ToString ();
	}

	public void checkMusic(){
		musicDis.text = musicSlider.value.ToString ();
	}

	public void checkSFX(){
		sfxDis.text = sfxSlider.value.ToString ();
	}
}
