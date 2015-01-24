﻿using UnityEngine;
using System.Collections;

public class GeraTile : MonoBehaviour {


	public GameObject[] tiles = new GameObject[4];
	public float tileDistance = 0.7f;

	int xLength;
	int yLength;
	//

	//1 - chao e teto
	//2 - parede
	//3 - plataforma
	//4 - portal

	int[,] tileMap = {
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,4,0,0,0,0,0,0,0,2},
		{2,3,3,3,3,3,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,3,3,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,3,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,3,3,0,0,3,3,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,3,3,3,3,3,0,0,3,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,3,3,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,3,3,3,3,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,3,3,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,3,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,3,3,3,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,3,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,3,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,3,3,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,3,3,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,3,3,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,3,3,3,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,0,0,2},
		{2,0,0,0,0,0,0,4,0,2},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1},
	};

	void Start()
	{
		xLength = tileMap.GetLength (0);
		yLength = tileMap.GetLength (1);

		GeraMap ();
	}

	public void GeraMap()
	{
		for (int i = xLength - 1, y = 0; i >= 0; i--, y++) 
		{
			for (int j = 0, x = 0; j < yLength; j++, x++) 
			{
				if(tileMap[i,j] != 0)
				{
					GameObject tile = Instantiate(tiles[tileMap[i,j]],new Vector2(x * tileDistance,y * tileDistance), transform.rotation) as GameObject;
					tile.transform.parent = transform;
				}
			}
		}
        GameObject.FindWithTag("Display").GetComponent<Display>().StartDisplay(30f);
	}
}