using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	//audio.clip = AudioClip for Pause\Play function.
	//audio.PlayOneShot(AudioClip) for multiple sounds.

	public SpriteRenderer OnSprite;
	public SpriteRenderer OffSprite;
	public bool isMute;
	public bool playing;
	private bool locutor, cont;
	private AudioSource audioSource;
	private GameConfig gameConfig;
	private AudioClip som, old;
	private float continueTime;

	void Awake() {
		gameConfig = GameObject.Find("GameConfig").GetComponent<GameConfig>();
		audioSource = gameObject.GetComponent<AudioSource>();
		locutor = false;
	}

	void Start()
	{
		isMute = gameConfig.isMute;
		if(isMute)
		{
			Off();
		}
		else
		{
			On();
		}
	}

	public void On()
	{
		isMute = false;
		audioSource.mute = false;
		gameConfig.isMute = false;
		//OnSprite.renderer.material.color = new Color(1f,1f,1f,1f);
		//OffSprite.renderer.material.color = new Color(1f,1f,1f,0.5f);
	}

	public void Off()
	{
		isMute = true;
		audioSource.mute = true;
		gameConfig.isMute = true;
		//OnSprite.renderer.material.color = new Color(1f,1f,1f,0.5f);
		//OffSprite.renderer.material.color = new Color(1f,1f,1f,1f);
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
