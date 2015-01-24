using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class FaseObject {
	public int lvl;
	public int[][] tiles;
}

public class AlwaysAlive : MonoBehaviour {

	public int fase = 0;
	public int faseMax = 0;
	public JSONObject fases;
	public FaseObject[] fasesObj;
	
	void Start()
	{
		DontDestroyOnLoad(gameObject);
		string stuff;
		stuff = File.ReadAllText(Application.dataPath +"/fase.txt");
		
		JSONObject j = new JSONObject(stuff);
		fases = j;

		//Debug.Log(fases);
		//Debug.Log(fases["mapas"]);

		fasesObj = new FaseObject[fases["mapas"].Count];
		int count = 0;
		int x = 0;

		foreach (var member in fases["mapas"].list) {
			Debug.Log(member);

			fasesObj[count] = new FaseObject();
			fasesObj[count].lvl = (int)member["fase"].n;
			fasesObj[count].tiles = new int[member["tiles"].Count][];

			x = 0;
			foreach (var tiles in member["tiles"].list) {
				Debug.Log(tiles);
				fasesObj[count].tiles[x] = Array.ConvertAll(tiles.str.ToCharArray(), c => (int)Char.GetNumericValue(c));
				var test = fasesObj[count].tiles[x];
				x++;
			}

			count++;
			//Debug.Log(fases["mapas"].keys[i]);
		}
	}

	public object getFase(){
		object fasi = fases["mapas"][fase]["tiles"];
		Debug.Log (fasi);
		return fasi;
	}
}
