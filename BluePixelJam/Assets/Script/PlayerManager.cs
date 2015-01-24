using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private Player[] players;

    public void OnSceneLoad() {
        Start();
    }
	// Use this for initialization
	void Start () {
        players = new Player[3];
        //for (int i = 0; i < players.Length; i++) {
        for (int i = 0; i < 1; i++) {
            while (players[i] == null) {
                players[i] = GameObject.FindWithTag("Player" + (i + 1)).GetComponent<Player>();
            }
            players[i].transform.parent = gameObject.transform;
        }
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>().setTarget(players[0].transform);
        activatePlayer(0);
	}

    private void activatePlayer(int player){
        //for (int i = 0; i < players.Length; i++) {
        for (int i = 0; i < 1; i++) {
               players[i].setActive(player == i);
        }
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>().setTarget(players[player].transform);
        GameObject.FindWithTag("Display").GetComponent<Display>().setDisplay(player+1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            activatePlayer(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            activatePlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            activatePlayer(2);
        }
	}
}
