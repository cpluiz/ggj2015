﻿using UnityEngine;
using System.Collections;

public class GameConfig : MonoBehaviour {
	[System.Serializable]
	public struct levelStruct{
		public bool unlocked;
	}

	//Store everything you need use between scenes.
	public bool isMute = false;
	public bool testMode;
	public bool unlockAll;
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	
	void Awake()
	{
        if (!testMode && Application.loadedLevelName == "Preload"){
            DontDestroyOnLoad(transform.gameObject);
            Application.LoadLevel("Load");
        }
	}
	void Start()
	{
		//Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

}