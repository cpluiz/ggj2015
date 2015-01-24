using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private Player[] players;

	// Use this for initialization
	void Start () {
        players = new Player[3];
        players[0] = GameObject.FindWithTag("Player1").GetComponent<Player>();
        players[1] = GameObject.FindWithTag("Player2").GetComponent<Player>();
        players[2] = GameObject.FindWithTag("Player3").GetComponent<Player>();
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().setTarget(players[0].transform);
        players[0].setActive(true);
	}

    private void activatePlayer(int player){
        for (int i = 0; i < players.Length; i++) {
            players[i].setActive(player == i);
        }
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().setTarget(players[player].transform);
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
