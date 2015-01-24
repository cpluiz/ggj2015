using UnityEngine;
using System.Collections;
using System.IO;

public class AlwaysAlive : MonoBehaviour {

	public int fase = 0;
	public int faseMax = 0;
	public JSONObject fases;
	
	void Start()
	{
		DontDestroyOnLoad(gameObject);
		string stuff;
		stuff = File.ReadAllText(Application.dataPath +"/fase.txt");
		
		JSONObject j = new JSONObject(stuff);
		fases = j;

	}

	//public object getFase(){
		//object fasi = fases["mapas"][fase]["tiles"];
		//Debug.Log (fasi);
		//return fasi;

	//}
}
