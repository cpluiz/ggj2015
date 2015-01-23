using UnityEngine;
using System.Collections;

public class BotaoIniciar : MonoBehaviour {

    void OnMouseDown() {
        Application.LoadLevel("Base");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            Application.LoadLevel("Base");
        }
    }

    void OnMouseEnter(){
        transform.localScale = new Vector3(1.2f,1.2f,1.2f);
    }

    void OnMouseExit(){
        transform.localScale = new Vector3(1,1,1);
    }
}
