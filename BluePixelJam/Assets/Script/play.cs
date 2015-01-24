using UnityEngine;
using System.Collections;
using System.IO;

public class play : MonoBehaviour {
	// Use this for initialization
	public JSONObject Fases;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goToLevel(int level) {
		if (holder.GetComponent<AlwaysAlive> ().faseMax >= lvl) {
			preparaFases();
            GameObject.FindWithTag("GameController").GetComponent<GameConfig>().setFase(level);
            Application.LoadLevel("Game");
		}

	}

	public Animator playBt;
	public Animator levels;
	public GameObject holder;
	public int lvl =0;

	public void goMenu(){
		playBt.SetBool("Hidden", true);
		levels.SetBool("Hidden", false);
	}
	

	public void preparaFases(){
		holder.GetComponent<AlwaysAlive>().fase = lvl;
		holder.GetComponent<AlwaysAlive>().getFase();
	}
}
