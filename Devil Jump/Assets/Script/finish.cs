using UnityEngine;
using System.Collections;

public class finish : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		Debug.Log ("AE");

		if(col.gameObject.name == "Player"){
			Application.LoadLevel(3);
		}
	}
}
