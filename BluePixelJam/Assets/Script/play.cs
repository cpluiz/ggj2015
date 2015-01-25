using UnityEngine;
using System.Collections;
using System.IO;

public class play : MonoBehaviour {
	// Use this for initialization
	public JSONObject Fases;
    private GameObject controller;
	void Start () {
        controller = GameObject.FindWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goToLevel(int level) {
        if (controller.GetComponent<AlwaysAlive>().faseMax >= level)
        {
            controller.GetComponent<GameConfig>().setFase(level);
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
	
}
