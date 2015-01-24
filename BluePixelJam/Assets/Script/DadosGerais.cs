using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class MyData : MonoBehaviour
{
	public int currentLevel = 0;
	public JSONObject Fases;
	
	void Start() {
		DontDestroyOnLoad(gameObject);
		string stuff;
		stuff = File.ReadAllText(Application.dataPath +"/fase.txt");
		
		JSONObject j = new JSONObject(stuff);
		Fases = j ["mapas"];
	}
}