using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	//audio.clip = AudioClip for Pause\Play function.
	//audio.PlayOneShot(AudioClip) for multiple sounds.

	public Image AudioSprite;
	public bool isMute;
	public bool playing;
	private bool locutor, cont;
	private AudioSource audioSource;
	public GameConfig gameConfig;
	private AudioClip som, old;
	private float continueTime;

	void Awake() {
		if (GameObject.FindWithTag("GameController") != null) {
			gameConfig = GameObject.FindWithTag("GameController").GetComponent<GameConfig> ();
		}
		audioSource = gameObject.GetComponent<AudioSource>();
		locutor = false;
	}

	void Start() {
		isMute = gameConfig != null ? gameConfig.isMute : false;

		if(isMute) {
			Off();
		} else {
			On();
		}
	}

	public void toggleAudio() {
		if (isMute) {
			On();
		} else {
			Off();
		}
	}

	public void On()
	{
		isMute = false;
		audioSource.mute = false;

		if (gameConfig != null) {
			gameConfig.isMute = false;
		}

        if (Application.loadedLevelName == "Game") {
			AudioSprite.sprite = Resources.Load<Sprite>("Interface/mute");
		}
	}

	public void Off()
	{
		isMute = true;
		audioSource.mute = true;

		if (gameConfig != null) {
			gameConfig.isMute = true;
		}

    	if (Application.loadedLevelName == "Game") {
			AudioSprite.sprite = Resources.Load<Sprite>("Interface/desmute");
		}
	}

	public float soundLength(){
		float tempo = 0;

		if(som != null){
			tempo = som.length;
		}

		return tempo;
	}

	public void playSound(string s){
		playSound(s, false, 0);
	}

	public void playSound(string s, bool loc){
		playSound(s, loc, 0);
	}

	public void playOneShot(string s){
		som = Resources.Load<AudioClip>("audio/"+s);
		audio.PlayOneShot(som);
	}

	public void playSound(string s, bool loc, float contTime)
	{
		som = Resources.Load<AudioClip>("audio/"+s);
		if(loc && !audio.isPlaying && !playing){
			locutor = true;
            audio.loop = true;
			if(contTime>0){
				audio.clip = old;
			}else{
				audio.clip = som;
			}
			audio.time = contTime;
			audio.Play();
		}else if(!audio.isPlaying && !playing || Time.timeScale == 0){
			if(som != null){
				if(Time.timeScale>0){
					locutor = false;
					playing = true;
				}
				audio.PlayOneShot(som);
				//print("chamou som "+s);
			}
		}
	}

	public void pauseSoud(){
		if((audio.isPlaying || playing) && locutor){
			cont = locutor;
			audio.Pause();
			if(audioSource.audio != null){
				old = audioSource.clip;
			}else{
				old = audio.clip;
			}
			continueTime = audio.time;
			audio.Stop();
			playing = false;
		}
		audio.Pause();
	}

	public float getTime(){
		return audio.time;
	}

	public void stopSound(){
		audio.Stop();
		if(Time.timeScale>0){playing = false;}
	}

	public void continueSound(){
		if(continueTime>0){
			playSound("",cont,continueTime);
		}
		continueTime = 0;
		cont = false;
	}

	private void FixedUpdate(){
		playing = audio.isPlaying;
	}

	public AudioClip getSound(){
		return som;
	}
}
