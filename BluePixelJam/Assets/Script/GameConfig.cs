﻿using UnityEngine;
using System.Collections;

public class GameConfig : MonoBehaviour {

	//Store everything you need use between scenes.
	public bool isMute = false;
	public bool testMode;
	public bool unlockAll;
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
    [SerializeField]
    private int faseAtual = 0;
	
	void Awake()
	{
        if (!testMode && Application.loadedLevelName == "Preload"){
            DontDestroyOnLoad(transform.gameObject);
            Application.LoadLevel("Load");
        }else{
            Application.LoadLevel("Preload");
        }
	}
	void Start()
	{
		//Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

    public void setFase(int fase) {
        faseAtual = fase;
    }
    public int getFase() {
        return faseAtual;
    }

}
